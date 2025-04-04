using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class SubjectGroupMember
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? StudentId { get; set; }
    [Required]
    public int SubjectGroupId { get; set;}

    // Navigation properties
    [ForeignKey("StudentId")]
    public AppUser? Student { get; set; }
    [ForeignKey("SubjectGroupId")]
    public SubjectGroup? SubjectGroup { get; set; }
}
