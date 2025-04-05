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
    /// <summary>Assigns student to subject groups based on their career/enrollment.</summary>
    /// <param name="student">Student to assign</param>
    /// <returns>Updated student with group assignments</returns>
    /// <exception cref="Exception">If no enrolled subjects or invalid subjects exist</exception>
    /// <remarks>Uses career data to find subjects, creates memberships.</remarks>
    public async Task<AppUser> AssignStudentToSubjectGroups(AppUser student)
    {
        // Get the student's career info to get the career's id
        var careerData = await unphuClient.GetStudentCareerAsync(student.UnphuId); ;
        if (careerData != null) student.CareerId = careerData.IdCarrera;

        if (student.CareerId != 0 && student.Id != null)
        {
            // Get all current enrolled subjects
            int year = DateTime.Now.Year;
            var period = await unphuClient.GetCurrentPeriodAsync();
            if (period == null) throw new Exception("Period couldn't be fetched");

            // Update last period login of user, so when period changes fetch all the subject groups again
            student.LastPeriodLoginId = period.Id;
            student.LastPeriodLoginYear = period.Year;

            var enrolledSubjects = await unphuClient.GetStudentEnrolledSubjectsAsync(student, year, period.Id);
            if (enrolledSubjects == null) throw new Exception("Student is not taking any classes");

            foreach (var subject in enrolledSubjects)
            {
                // Check if subject exits on database before trying to make a duplicate
                if (context.SubjectGroups.Any(x => x.Code == subject.Code)) continue;
                await context.SubjectGroups.AddAsync(subject);  // Add new subject
            }
            await context.SaveChangesAsync();   // Save all subjects to have their ids

            foreach (var group in enrolledSubjects)
            {
                // Get the tracked subject (the one with and id generated by the database)
                var trackedSubject = await context.SubjectGroups.FirstOrDefaultAsync(x => x.Code == group.Code);
                if (trackedSubject == null) throw new Exception($"{group.Code} doesn't exist"); // Avoid untracked subjects

                // Create a new entry that represents the user belonging to a subject
                var member = new SubjectGroupMember
                {
                    StudentId = student.Id,
                    SubjectGroupId = trackedSubject.Id
                };
                await context.SubjectGroupMembers.AddAsync(member); // Save member
            }
            await context.SaveChangesAsync();   // Save all changes
        }
        return student;
    }

    public async Task<SubjectGroup?> DeleteByIdAsync(int id)
    {
        var subjectGroup = await context.SubjectGroups.FindAsync(id);
        if (subjectGroup == null) return null;

        context.SubjectGroups.Remove(subjectGroup);
        await context.SaveChangesAsync();

        return subjectGroup;
    }

    public async Task<SubjectGroup?> GetByIdAsync(int id)
    {
        var subjectGroup = await context.SubjectGroups.FindAsync(id);
        return subjectGroup;
    }

    /// <summary>Gets a student's enrolled subject groups.</summary>
    /// <param name="username">Student ID to lookup</param>
    /// <returns>List of enrolled subjects or null if none found</returns>
    /// <remarks>Finds user, fetches memberships, and maps to DTOs.</remarks>
    public async Task<List<SubjectGroupDto>?> GetUserSubjectGroups(string username)
    {
        // Get user by username
        var user = await userManager.FindByNameAsync(username);
        if (user == null) return null;  // Avoid no users

        // Get all subject the user is currently taking
        var subjectMembers = await context.SubjectGroupMembers  
            .Include(x => x.SubjectGroup)
            .Where(x => x.StudentId == user.Id)
            .ToListAsync();
    
        // Get only the subjects from the data returned by database, and map them to a dto
        var subjectGroups = subjectMembers.Select(x => mapper.Map<SubjectGroupDto>(x.SubjectGroup));

        return subjectGroups.ToList();  // Return list
    }
}
