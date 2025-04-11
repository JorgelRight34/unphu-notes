using System;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<SubjectGroup> SubjectGroups { get; set; }
    public DbSet<SubjectGroupMember> SubjectGroupMembers { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<NoteFile> NoteFiles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Note>()
            .HasMany(n => n.Comments)
            .WithOne(c => c.Note)
            .HasForeignKey(c => c.NoteId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Note>()
            .HasMany(n => n.NoteFiles)
            .WithOne(f => f.Note)
            .HasForeignKey(f => f.NoteId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<SubjectGroup>()
            .HasMany(s => s.Members)
            .WithOne(m => m.SubjectGroup)
            .HasForeignKey(m => m.SubjectGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        var roles = new List<IdentityRole>
        {
            new IdentityRole { Id ="Admin", Name="Admin", NormalizedName="ADMIN"},
            new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER"},
            new IdentityRole { Id="Teacher", Name="Teacher", NormalizedName="TEACHER"}
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }
}
