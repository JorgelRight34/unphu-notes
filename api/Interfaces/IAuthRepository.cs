using System;
using api.Data;
using api.DTOs.User;
using Google.Apis.Auth;

namespace api.Interfaces;

public interface IAuthRepository
{
    Task<UserDto?> SignInFromGoogleTokenAsync(GoogleJsonWebSignature.Payload payload);
}
