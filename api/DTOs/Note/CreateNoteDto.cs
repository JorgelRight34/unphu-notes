using System;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Note;

public class CreateNoteDto
{
    [Required]
    public int SubjectGroupId { get; set; }
    [Required]
    public List<IFormFile>? Files { get; set; }
    [Required]
    public int Week { get; set; }
    [Required]
    public DateTime DateTime { get; set; } = DateTime.Now;
}
