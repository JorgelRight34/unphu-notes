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
    public string? Id { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public int CareerId { get; set; }
    public string? Token { get; set; }
}
