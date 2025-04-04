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

        return student;
    }

    public async Task<AppUser?> SignInFromGoogleTokenAsync(GoogleJsonWebSignature.Payload payload)
    {
        // Extract username from email and check if it's valid;
        var username = GetUsernameFromGoogleEmail(payload.Email);

        // Check if user exists
        var existingUser = await userManager.FindByNameAsync(username);
        if (existingUser != null)
        {
            await CreateFullStudent(existingUser);  // temporary
            await userManager.UpdateAsync(existingUser); // temporary
            if (existingUser.ProfilePic != payload.Picture)
            {
                existingUser.ProfilePic = payload.Picture;
                await userManager.UpdateAsync(existingUser);
            }
            return existingUser;
        }

        // Create user
        var user = mapper.Map<AppUser>(payload);
        await CreateFullStudent(user);
        var result = await userManager.CreateAsync(user);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
            return user;
        }

        foreach (var error in result.Errors) Console.WriteLine($"${error.Description}");

        return null;
    }

    public string GetUsernameFromGoogleEmail(string email)
    {
        var username = email.Split("@")[0];
        var regex = @"[a-z]{2}\d{2}\-\d{4}";
        bool isMatch = Regex.IsMatch(username.Trim(), regex);
        if (!isMatch) throw new Exception($"Invalid username {username}");

        return username;
    }
}
