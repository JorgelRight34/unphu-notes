using System;
using api.Data;
using api.DTOs.User;
using api.Models;
using Google.Apis.Auth;

namespace api.Interfaces;

public interface IAuthRepository
{
    Task<AppUser?> SignInFromGoogleTokenAsync(GoogleJsonWebSignature.Payload payload);
    Task<AppUser?> CreateFullStudent(AppUser student);
}
