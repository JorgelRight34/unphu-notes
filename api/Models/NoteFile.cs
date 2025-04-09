using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class NoteFile
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required string PublicId { get; set; }
    [Required]
    public required string Url { get; set; }
    public int NoteId { get; set; }

    // Navigation properties
    [ForeignKey("NoteId")]
    public Note? Note { get; set;}
}
