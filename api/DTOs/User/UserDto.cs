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
    public string? Token { get; set; }
}
