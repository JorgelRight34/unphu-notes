using System;
using api.Data;
using api.DTOs.Note;
using api.DTOs.SubjectGroup;
using api.DTOs.SubjectGroupMember;
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
        // User
        CreateMap<GoogleJsonWebSignature.Payload, AppUser>();
        CreateMap<AppUser, UserDto>();

        // SubjectGroup
        CreateMap<SubjectGroup, SubjectGroupDto>();

        // SubjectGroupMember
        CreateMap<SubjectGroupMember, SubjectGroupMemberDto>();

        // Notes
        CreateMap<Note, NoteDto>();
        CreateMap<CreateNoteDto, Note>();
    }
}
