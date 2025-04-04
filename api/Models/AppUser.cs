using System;
using api.Validators;
using Microsoft.AspNetCore.Identity;

namespace api.Models;

public class AppUser : IdentityUser
{
    public string? ProfilePic { get; set; }
    public int UnphuId { get; set; }
    public int CareerId { get; set; }
}
