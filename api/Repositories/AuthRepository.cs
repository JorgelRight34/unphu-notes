using System;
using api.Data;
using api.DTOs.User;
using api.Interfaces;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace api.Repositories;

public class AuthRepository(UserManager<AppUser> userManager, ITokenService tokenService) : IAuthRepository
{
    public async Task<UserDto?> SignInFromGoogleTokenAsync(GoogleJsonWebSignature.Payload payload)
    {
        var username = payload.Email.Split("@")[0];
        var regex = @"\[a-z]{2}-\d{4}";
        bool isMatch = Regex.IsMatch(username, regex);
        if (!isMatch) throw new Exception("Invalid username");


        // Check if user exists
        var existingUser = await userManager.FindByNameAsync(username);
        if (existingUser != null)
        {
            if (existingUser.ProfilePic != payload.Picture)
            {
                existingUser.ProfilePic = payload.Picture;
                await userManager.UpdateAsync(existingUser);
            }
            return new UserDto
            {
                Username = existingUser.UserName,
                Token = tokenService.CreateToken(existingUser)
            };
        }

        // Create user
        var user = new AppUser
        {
            UserName = username,
            NormalizedUserName = username.ToUpper(),
            Email = payload.Email,
            ProfilePic = payload.Picture
        };

        var result = await userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
            return new UserDto
            {
                Username = user.UserName,
                Token = tokenService.CreateToken(user)
            };
        }

        foreach (var error in result.Errors)
        {
            Console.WriteLine($"pasoooooooooooooooooooo ${error.Description}");
        }

        return null;
    }
}
