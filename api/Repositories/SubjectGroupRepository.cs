using System;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class SubjectGroupRepository(
    ApplicationDbContext context,
    IUNPHUClient unphuClient,
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

            // Get only the subjects the student is currently taking
            var currentSubjects = enrolledSubjects
                .Where(x => x.Observation == "EC")
                .Select(x => new SubjectGroup
                {
                    Code = x.GroupSubjectCode,
                    Name = x.SubjectName,
                    Credits = x.Credits,
                    ScheduleText = x.ScheduleText,
                    TeacherName = x.Teacher.TrimStart()
                })
                .ToList();

            foreach (var subject in currentSubjects)
            {
                // Check if subject exits on database before trying to make a duplicate
                if (context.SubjectGroups.Any(x => x.Code == subject.Code)) continue;
                await context.SubjectGroups.AddAsync(subject);  // Add new subject
            }
            await context.SaveChangesAsync();   // Save all subjects to have their ids

            foreach (var group in currentSubjects)
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

    /// <summary>
    /// Deletes a subject group by id.
    /// This method is used to delete a subject group by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<SubjectGroup?> DeleteByIdAsync(int id)
    {
        var subjectGroup = await context.SubjectGroups.FindAsync(id);
        if (subjectGroup == null) return null;

        context.SubjectGroups.Remove(subjectGroup);
        await context.SaveChangesAsync();

        return subjectGroup;
    }

    /// <summary>Gets a subject group by id.</summary>
    /// <param name="id">Id of the subject group to get</param>
    public async Task<SubjectGroup?> GetByIdAsync(int id)
    {
        var subjectGroup = await context.SubjectGroups.FindAsync(id);
        return subjectGroup;
    }

    /// <summary>Gets a subject group member by username and subject group id.</summary>
    /// <param name="username">Username of the student</param>
    public async Task<SubjectGroupMember?> GetGroupMember(string username, int subjectGroupId)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user == null) throw new Exception("User not found");

        var groupMember = await context.SubjectGroupMembers
            .Where(x => x.StudentId == user.Id && x.SubjectGroupId == subjectGroupId)
            .FirstOrDefaultAsync();

        return groupMember;
    }

    /// <summary>Gets a student's enrolled subject groups.</summary>
    /// <param name="username">Student ID to lookup</param>
    /// <returns>List of enrolled subjects or null if none found</returns>
    /// <remarks>Finds user, fetches memberships, and maps to DTOs.</remarks>
    /// <exception cref="Exception">Thrown if user not found</exception>
    /// <returns>List of SubjectGroupDto or null if none found</returns>
    public async Task<List<SubjectGroup?>?> GetUserSubjectGroups(string username)
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
        var subjectGroups = subjectMembers.Select(x => x.SubjectGroup);

        return subjectGroups.ToList();  // Return list
    }
}
