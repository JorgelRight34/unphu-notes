using System;
using api.Data;
using api.DTOs.User;
using api.Interfaces;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using AutoMapper;
using api.Models;
using api.Clients;

namespace api.Repositories;

public class AuthRepository(
    UserManager<AppUser> userManager,
    ISubjectGroupRepository subjectGroupRepository,
    IMapper mapper,
    IUNPHUClient unphuClient
) : IAuthRepository
{
    /// <summary>
    /// Registers a new student user with UNPHU system data.
    /// </summary>
    /// <param name="student"><see cref="AppUser"/> with at least UserName.</param>
    /// <returns>Created <see cref="AppUser"/> with UNPHU data and groups, or null if failed.</returns>
    /// <exception cref="Exception">Thrown if user can't be added to 'User' role.</exception>
    /// <remarks>
    /// Fetches UNPHU data, sets UnphuId, creates user, assigns role, and adds to subject groups.
    /// </remarks>
    public async Task<AppUser?> CreateFullStudent(AppUser student)
    {
        if (student.UserName == null) throw new Exception("Empty username");  // Avoid empty usernames

        // Get all the data of the student from UNPHU's api, includes career, names, etc.
        var studentData = await unphuClient.GetStudentAsync(student.UserName);
        if (studentData == null) throw new Exception("No student data from UNPHU");
        student.UnphuId = studentData.Id;

        var result = await userManager.CreateAsync(student);   // Save user on database
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(student, "User");  // Add to role
        }
        else
        {
            throw new Exception("User couldn't be added to role");
        }

        // Assign user to all the subjects the user is taking right now
        student = await subjectGroupRepository.AssignStudentToSubjectGroups(student);
        return student;
    }

    /// <summary>
    /// Signs in or creates a user using Google token payload.
    /// </summary>
    /// <param name="payload">Valid Google ID token payload.</param>
    /// <returns>Signed-in <see cref="AppUser"/> with UNPHU data, or null if failed.</returns>
    /// <remarks>
    /// Uses Google email as username. Updates existing users or creates new student accounts.
    /// </remarks>
    public async Task<AppUser?> SignInFromGoogleTokenAsync(GoogleJsonWebSignature.Payload payload)
    {
        // Extract username from email and check if it's valid;
        var username = GetUsernameFromGoogleEmail(payload.Email);

        // Check if user exists, if exist apply pending changes such as updating profile pic
        var existingUser = await CheckIfUserExistsAndApplyPendingChangesAsync(username, payload);
        if (existingUser != null) return existingUser;

        // Create user from scratch
        var user = mapper.Map<AppUser>(payload);
        user.ProfilePic = payload.Picture;  // Set profile pic
        user.UserName = username;
        user = await CreateFullStudent(user);  // Fill all the missing information about user

        return user;
    }

    /// <summary>Extracts and validates a UNPHU student ID from a Google email.</summary>
    /// <param name="email">Google email (e.g., ab12-3456@unphu.edu.do)</param>
    /// <returns>Validated username (e.g., ab12-3456)</returns>
    /// <exception cref="Exception">Thrown if format doesn't match [a-z]{2}\d{2}-\d{4}</exception>
    /// <remarks>Splits email and validates ID format with regex.</remarks>
    public string GetUsernameFromGoogleEmail(string email)
    {
        var username = email.Split("@")[0]; // Get the UNPHU student id
        var regex = @"[a-z]{2}\d{2}\-\d{4}";    // Student id pattern XX00-0000
        bool isMatch = Regex.IsMatch(username, regex);   // Check if username matches
        if (!isMatch) throw new Exception($"Invalid username {username}");

        return username;
    }

    /// <summary>Finds and updates a user with Google data if they exist.</summary>
    /// <param name="username">UNPHU student ID to search</param>
    /// <param name="payload">Google user data</param>
    /// <returns>Updated <see cref="AppUser"/> or null if not found</returns>
    /// <remarks>Updates profile picture if different from Google's.</remarks>
    public async Task<AppUser?> CheckIfUserExistsAndApplyPendingChangesAsync(string username, GoogleJsonWebSignature.Payload payload)
    {  
        // Try to get an existing user
        var existingUser = await userManager.FindByNameAsync(username);
        if (existingUser != null)
        {   
            // If user exists, check if their profile pic changed
            if (existingUser.ProfilePic != payload.Picture)
            {
                // If profile pic changed, then update the user's profile pic
                existingUser.ProfilePic = payload.Picture;
                await userManager.UpdateAsync(existingUser);
            }
            var period = await unphuClient.GetCurrentPeriodAsync();
            if (period == null) throw new Exception("Current period could not be get from UNPHU API");

            if (period.Year >= existingUser.LastPeriodLoginYear && existingUser.LastPeriodLoginId < period.Id)
            {
                // If first time user logs in the app on current academic period, then update all user's current subject groups
                existingUser = await subjectGroupRepository.AssignStudentToSubjectGroups(existingUser);
            }

            return existingUser;
        }
        return null;    // There was not an existing user
    }

    /// <summary>Gets a user by their UNPHU student ID.</summary>
    /// <param name="username">Student ID to lookup</param>
    /// <returns>The matching <see cref="AppUser"/> or null</returns>
    public async Task<AppUser?> GetByUsernameAsync(string username)
    {
        var user = await userManager.FindByNameAsync(username);
        return user;
    }
}
