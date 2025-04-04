using System;
using api.Data;
using api.Models;

namespace api.Interfaces;

public interface ISubjectGroupRepository
{
    Task<AppUser> AssignStudentToSubjectGroups(AppUser student);
}
