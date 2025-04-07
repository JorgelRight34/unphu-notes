using System;

namespace api.DTOs.UNPHUClient;

public class EnrolledSubjectDto
{
    public string? GroupSubjectCode { get; set; }
    public string? SubjectName { get; set; }
    public int Semester { get; set; }
    public int Credits { get; set; }
    public string? ScheduleText { get; set; }
    public string? Teacher { get; set; }
    public string? Observation { get; set; }
}
