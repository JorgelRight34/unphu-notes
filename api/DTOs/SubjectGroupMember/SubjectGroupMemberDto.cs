using System;
using System.ComponentModel.DataAnnotations;
using api.DTOs.SubjectGroup;
using api.DTOs.User;

namespace api.DTOs.SubjectGroupMember;

public class SubjectGroupMemberDto
{   
    public int Id { get; set; }
    [Required]
    public string? StudentId { get; set; }
    [Required]
    public int SubjectGroupId { get; set;}
    public UserDto? Student { get; set; }
}
