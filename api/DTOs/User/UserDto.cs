using System;
using api.Data;

namespace api.DTOs.User;

public class UserDto
{
    public AppUser User { get; set; }
    public string? Token { get; set; }
}
