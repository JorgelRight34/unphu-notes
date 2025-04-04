using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Data;

namespace api.Models;

public class SubjectGroup
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Code { get; set; }
    [Required]
    public string? Name { get; set; }
    public int Credits { get; set; }
    public string? TeacherId { get; set; }

    // Navigation properties
    [ForeignKey("TeacherId")]
    public AppUser? Teacher { get; set; }
}
