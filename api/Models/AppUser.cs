using System;
using api.Validators;
using Microsoft.AspNetCore.Identity;

namespace api.Data;

public class AppUser : IdentityUser
{
    public string? ProfilePic { get; set; }
}
