using System;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Note;

public class NoteDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int SubjectGroupId { get; set; }
    [Required]
    public string? StudentId { get; set; }
    [Required]
    [Url]
    public string? Url { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
}
