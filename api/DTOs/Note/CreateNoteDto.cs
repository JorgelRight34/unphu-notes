using System;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Note;

public class CreateNoteDto
{
    [Required]
    public int SubjectGroupId { get; set; }
    [Required]
    public DateTime DateTime { get; set; } = DateTime.Now;
}
