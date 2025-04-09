using System;
using System.ComponentModel.DataAnnotations;
using api.Data;
using api.Validators;

namespace api.DTOs.User;

public class UserDto
{
    [Required]
    [StudentId]
    public string? Username { get; set; }
    [Required]   
    public string? ProfilePic { get; set; }
    public int UnphuId { get; set; }
    public int CareerId { get; set; }
    public int LastPeriodLoginYear { get; set; }
    public int LastPeriodLoginId { get; set; }
    public string? Token { get; set; }
}
