using System;
using api.Validators;
using Microsoft.AspNetCore.Identity;

namespace api.Models;

public class AppUser : IdentityUser
{
    public string? ProfilePic { get; set; }
    public string? FullName { get; set; }
    public int UnphuId { get; set; }
    public int CareerId { get; set; }
    public int LastPeriodLoginYear { get; set; }
    public int LastPeriodLoginId { get; set; }
}
