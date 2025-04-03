using System;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.User;

public class GoogleLoginRequestDto
{
    [Required]
    public string? Token { get; set; }
}
