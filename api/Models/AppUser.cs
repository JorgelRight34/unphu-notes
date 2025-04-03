using System;
using api.Validators;
using Microsoft.AspNetCore.Identity;

namespace api.Data;

public class AppUser : IdentityUser
{
    [StudentId]
    public new string? UserName { get; set; }
    public string? ProfilePic { get; set; }
}
