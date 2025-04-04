using System;
using api.Data;
using api.DTOs.UNPHUClient;
using api.DTOs.User;
using api.Models;
using AutoMapper;
using Google.Apis.Auth;

namespace api.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<GoogleJsonWebSignature.Payload, AppUser>();
        CreateMap<AppUser, UserDto>();
    }
}
