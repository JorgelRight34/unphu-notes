using System;
using api.Data;
using api.DTOs.User;
using api.Interfaces;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;

namespace api.Repositories;

public class AuthRepository(UserManager<AppUser> userManager, ITokenService tokenService) : IAuthRepository
{
    public async Task<UserDto?> SignInFromGoogleTokenAsync(GoogleJsonWebSignature.Payload payload)
    {
        var username = payload.Email.Split("@")[0];

        // Check if user exists
        var existingUser = await userManager.FindByNameAsync(username);
        if (existingUser != null) {
            if (existingUser.ProfilePic != payload.Picture) 
            {
                existingUser.ProfilePic = payload.Picture;
                await userManager.UpdateAsync(existingUser);
            }
            return new UserDto
            {
                User = existingUser,
                Token = tokenService.CreateToken(existingUser)
            };
        }

        // Create user
        var user = new AppUser
        {
            UserName = username,
            Email = payload.Email,
            ProfilePic = payload.Picture
        };

        var result = await userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
            return new UserDto
            {
                User = user,
                Token = tokenService.CreateToken(user)
            };
        }

        return null;
    }
}
