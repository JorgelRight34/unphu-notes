using System;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.SubjectGroup;

public class SubjectGroupDto
{
    public int Id { get; set; }
    [Required]
    public string? Code { get; set; }
    [Required]
    public string? Name { get; set; }
    public int Credits { get; set; }
    public string? TeacherId { get; set; }
}
