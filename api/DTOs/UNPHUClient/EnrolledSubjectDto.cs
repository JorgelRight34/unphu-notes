using System;

namespace api.DTOs.UNPHUClient;

public class EnrolledSubjectDto
{
    public required string GroupSubjectCode { get; set; }
    public required string SubjectName { get; set; }
    public int Semester { get; set; }
    public int Credits { get; set; }
    public required string ScheduleText { get; set; }
    public required string Teacher { get; set; }
    public required string Observation { get; set; }
}
