using System;
using api.Data;

namespace api.Interfaces;

public interface ISubjectGroupRepository
{
    Task AssignStudentToSubjectGroups(AppUser student);
}
