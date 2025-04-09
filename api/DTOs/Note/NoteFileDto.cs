using System;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Note;

public class NoteFileDto
{
    [Required]
    public required string PublicId { get; set; }
    [Required]
    public required string Url { get; set; }
    public int NoteId { get; set; }
}
