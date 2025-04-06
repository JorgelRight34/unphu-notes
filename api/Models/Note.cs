using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class Note
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int SubjectGroupId { get; set; }
    [Required]
    public string? StudentId { get; set; }
    [Url]
    public string? Url { get; set; }
    public string? PublicId { get; set; }
    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    // Navigation properties
    [ForeignKey("SubjectGroupId")]
    public SubjectGroup? SubjectGroup { get; set; }
    [ForeignKey("StudentId")]
    public AppUser? Student { get; set; }
}
