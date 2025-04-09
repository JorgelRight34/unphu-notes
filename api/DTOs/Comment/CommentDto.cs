using System;
using System.ComponentModel.DataAnnotations;
using api.DTOs.User;
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
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public UserDto? Author { get; set; }
}
