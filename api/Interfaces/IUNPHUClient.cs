using System;
using System.Text.Json;
using api.Data;
using api.DTOs.UNPHUClient;
using api.Models;

namespace api.Interfaces;

public interface IUNPHUClient
{
    Task<StudentDataDto?> GetStudentAsync(string username);
    Task<PeriodDto?> GetCurrentPeriodAsync();
    Task<StudentCareerDto?> GetStudentCareerAsync(int id);
    Task<List<EnrolledSubjectDto>?> GetStudentEnrolledSubjectsAsync(AppUser user, int year, int periodId);
}
