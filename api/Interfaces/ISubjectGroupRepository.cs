using System;
using api.Data;
using api.DTOs.SubjectGroup;
using api.Models;

namespace api.Interfaces;

public interface ISubjectGroupRepository
{
    Task<AppUser> AssignStudentToSubjectGroups(AppUser student);
    Task<SubjectGroupMember?> GetGroupMember(string username, int subjectGroupId);
    Task<List<SubjectGroupMember>> GetGroupMembers(int subjectGroupId, string username);
    Task<List<SubjectGroup?>?> GetUserSubjectGroups(string username);
    Task<SubjectGroup?> GetByIdAsync(int id, string username);
    Task<SubjectGroup?> DeleteByIdAsync(int id, string username);
}
