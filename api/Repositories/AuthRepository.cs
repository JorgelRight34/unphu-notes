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
    public async Task<AppUser?> CreateFullStudent(AppUser student)
    {
        if (student.UserName == null) return null;

        var studentData = await unphuClient.GetStudentAsync(student.UserName);
        if (studentData == null) return null;
        student.UnphuId = studentData.Id;

        var result = await userManager.CreateAsync(student);   // Save user on database
        if (result.Succeeded) {
            await userManager.AddToRoleAsync(student, "User");
        } else {
            throw new Exception("User couldn't be added to role");
        }

        student = await subjectGroupRepository.AssignStudentToSubjectGroups(student);

        return student;
    }

    public async Task<AppUser?> SignInFromGoogleTokenAsync(GoogleJsonWebSignature.Payload payload)
    {
        // Extract username from email and check if it's valid;
        var username = GetUsernameFromGoogleEmail(payload.Email);

        // Check if user exists, if exist apply pending changes such as updating profile pic
        var existingUser = await CheckIfUserExistsAndApplyPendingChangesAsync(username, payload);
        if (existingUser != null) return existingUser;

        // Create user from scratch
        var user = mapper.Map<AppUser>(payload);
        user = await CreateFullStudent(user);  // Fill all the missing information about user

        return user;
    }

    public string GetUsernameFromGoogleEmail(string email)
    {
        var username = email.Split("@")[0]; // Get the UNPHU student id
        var regex = @"[a-z]{2}\d{2}\-\d{4}";    // Student id pattern XX00-0000
        bool isMatch = Regex.IsMatch(username, regex);   // Check if username matches
        if (!isMatch) throw new Exception($"Invalid username {username}");

        return username;
    }

    public async Task<AppUser?> CheckIfUserExistsAndApplyPendingChangesAsync(string username, GoogleJsonWebSignature.Payload payload)
    {
        var existingUser = await userManager.FindByNameAsync(username);
        if (existingUser != null)
        {
            if (existingUser.ProfilePic != payload.Picture)
            {
                existingUser.ProfilePic = payload.Picture;
                await userManager.UpdateAsync(existingUser);
            }
            return existingUser;
        }
        return null;
    }

    public async Task<AppUser?> GetByUsernameAsync(string username)
    {
         var user = await userManager.FindByNameAsync(username);
         return user;
    }
}
