using System;
using api.Clients;
using api.Data;
using api.DTOs.SubjectGroup;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class SubjectGroupRepository(
    ApplicationDbContext context, 
    IUNPHUClient unphuClient, 
    IMapper mapper, 
    UserManager<AppUser> userManager
) : ISubjectGroupRepository
{
    public async Task<AppUser> AssignStudentToSubjectGroups(AppUser student)
    {

        var careerData = await unphuClient.GetStudentCareerAsync(student.UnphuId); ;
        if (careerData != null) student.CareerId = careerData.IdCarrera;

        if (student.CareerId != 0 && student.Id != null)
        {
            var enrolledSubjects = await unphuClient.GetStudentEnrolledSubjectsAsync(student);
            if (enrolledSubjects == null) throw new Exception("Student is not taking any classes");

            foreach (var subject in enrolledSubjects)
            {
                if (context.SubjectGroups.Any(x => x.Code == subject.Code)) continue;
                await context.SubjectGroups.AddAsync(subject);
            }

            await context.SaveChangesAsync();
            foreach (var group in enrolledSubjects)
            {
                var trackedSubject = await context.SubjectGroups.FirstOrDefaultAsync(x => x.Code == group.Code);
                if (trackedSubject == null) throw new Exception($"{group.Code} doesn't exist");

                var member = new SubjectGroupMember
                {
                    StudentId = student.Id,
                    SubjectGroupId = trackedSubject.Id
                };

                await context.SubjectGroupMembers.AddAsync(member);
            }
            await context.SaveChangesAsync();
        }

        return student;
    }

    public async Task<List<SubjectGroupDto>?> GetUserSubjectGroups(string username)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user == null) return null;

        var subjectMembers = await context.SubjectGroupMembers
            .Include(x => x.SubjectGroup)
            .Where(x => x.StudentId == user.Id)
            .ToListAsync();
            
        var subjectGroups = subjectMembers.Select(x => mapper.Map<SubjectGroupDto>(x.SubjectGroup));

        return subjectGroups.ToList();
    }
}
