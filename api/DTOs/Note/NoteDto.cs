using System;
using System.ComponentModel.DataAnnotations;
using api.DTOs.SubjectGroup;
using api.DTOs.User;
using api.Models;

namespace api.DTOs.Note;

public class NoteDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    public int SubjectGroupId { get; set; }
    [Required]
    public string? StudentId { get; set; }
    [Url]
    public string? Url { get; set; }
    [Required]
    public int Week { get; set; }
    public DateTime Date { get; set; }

    public UserDto? Student { get; set; }
    public IEnumerable<NoteFileDto> NoteFiles { get; set; } = new List<NoteFileDto>();
}
