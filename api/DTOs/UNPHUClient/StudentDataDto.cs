using System;

namespace api.DTOs.UNPHUClient;

public class StudentDataDto
{
    public int Id { get; set; }
    public string? Names { get; set; }
    public string? Username { get; set; }
    public string? Career { get; set; }
    public string? Email { get; set; }
    public string? Enclosure { get; set; }
    public string? Enrollment { get; set; }
}   
