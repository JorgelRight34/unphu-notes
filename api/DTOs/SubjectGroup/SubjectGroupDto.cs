using System;
using System.ComponentModel.DataAnnotations;
using api.DTOs.Note;
using api.DTOs.SubjectGroupMember;
using api.DTOs.User;

namespace api.DTOs.SubjectGroup;

public class SubjectGroupDto
{
    public int Id { get; set; }
    [Required]
    public string? Code { get; set; }
    [Required]
    public string? Name { get; set; }
    public int Credits { get; set; }
    [Required]
    public string? TeacherName { get; set; }
    [Required]
    public string? ScheduleText { get; set; }
    public string? TeacherId { get; set; }
    public List<SubjectGroupMemberDto> Members { get; set; } = new List<SubjectGroupMemberDto>();
    public List<NoteDto> Notes { get; set; } = new List<NoteDto>();
}
