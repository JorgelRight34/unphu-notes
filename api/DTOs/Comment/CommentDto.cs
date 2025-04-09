using System;
using System.ComponentModel.DataAnnotations;
using api.Models;

namespace api.DTOs.Comment;

public class CommentDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int NoteId { get; set; }
    [Required]
    public string? AuthorId { get; set; }
    public string? Content { get; set; }

    // Navigation properties
    public AppUser? Author { get; set; }
}
