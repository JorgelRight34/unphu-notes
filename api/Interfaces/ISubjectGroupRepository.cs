using System;
using api.Data;
using api.DTOs.SubjectGroup;
using api.Models;

namespace api.Interfaces;

public interface ISubjectGroupRepository
{
    Task<AppUser> AssignStudentToSubjectGroups(AppUser student);
    Task<List<SubjectGroupDto>?> GetUserSubjectGroups(string username);
    Task<SubjectGroup?> GetByIdAsync(int id);
    Task<SubjectGroup?> DeleteByIdAsync(int id);
}
