using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int SubjectGroupId { get; set; }
    [Required]
    public int NoteId { get; set; }
    [Required]
    public string? AuthorId { get; set; }
    [Required]
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public Note? Note { get; set; }
    public AppUser? Author { get; set; }
    public SubjectGroup? SubjectGroup { get; set; }
}
