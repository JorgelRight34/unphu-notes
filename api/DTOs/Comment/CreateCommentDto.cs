using System;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Comment;

public class CreateCommentDto
{
    [Required]
    public int NoteId { get; set; }
    [Required]
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
