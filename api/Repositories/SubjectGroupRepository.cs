using System;
using api.Clients;
using api.Data;
using api.Interfaces;
using api.Models;

namespace api.Repositories;

public class SubjectGroupRepository(ApplicationDbContext context, IUNPHUClient unphuClient) : ISubjectGroupRepository
{
    public async Task<AppUser> AssignStudentToSubjectGroups(AppUser student)
    {

        var careerData = await unphuClient.GetStudentCareerAsync(student.UnphuId);
        Console.WriteLine($"Carrer data {careerData?.IdCarrera}");
        if (careerData != null) student.CareerId = careerData.IdCarrera;

        if (student.CareerId != 0 && student.Id != null)
        {
            var enrolledSubjects = await unphuClient.GetStudentEnrolledSubjectsAsync(student);
            if (enrolledSubjects == null) throw new Exception("Student is not taking any classes");
            foreach (var subject in enrolledSubjects)
            {
                if (context.SubjectGroups.Any(x => x.Code == subject.Code && x.TeacherId == subject.TeacherId));
                await context.SubjectGroups.AddAsync(subject);
            }
            await context.SaveChangesAsync();
            foreach (var group in enrolledSubjects)
            {
                var member = new SubjectGroupMember
                {
                    StudentId = student.Id,
                    SubjectGroupId = group.Id
                };
                Console.WriteLine($"studentid: {member.StudentId}");
                Console.WriteLine($"group id {member.SubjectGroupId}");
                await context.SubjectGroupMembers.AddAsync(member);
            }
            await context.SaveChangesAsync();
        }

        return student;
    }
}
