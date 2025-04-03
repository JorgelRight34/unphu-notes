using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var roles = new List<IdentityRole>
        {
            new IdentityRole { Id ="Admin", Name="Admin", NormalizedName="ADMIN"},
            new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER"},
            new IdentityRole { Id="Teacher", Name="Teacher", NormalizedName="TEACHER"}
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }
}
