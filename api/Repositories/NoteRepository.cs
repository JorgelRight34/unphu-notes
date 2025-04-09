using System;
using api.Data;
using api.DTOs.Note;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class NoteRepository(
    ApplicationDbContext context,
    IFileUploadService fileUploadService,
    ISubjectGroupRepository subjectGroupRepository,
    IMapper mapper
) : INoteRepository
{
    /// <summary>
    /// Creates a new note from the provided data transfer object (DTO).
    /// </summary>
    /// <param name="createNoteDto">The <see cref="CreateNoteDto"/> containing the data for the new note.</param>
    /// <returns>
    /// A <see cref="NoteDto"/> representing the created note.
    /// </returns>
    public async Task<Note> CreateAsync(CreateNoteDto createNoteDto, string username)
    {
        var member = await subjectGroupRepository.GetGroupMember(username, createNoteDto.SubjectGroupId);
        if (member == null) throw new Exception("You are not a member");

        var note = mapper.Map<Note>(createNoteDto);
        note.StudentId = member.StudentId;

        if (createNoteDto.File != null)
        {
            var fileResult = await fileUploadService.AddFileAsync(createNoteDto.File);
            if (fileResult.Error != null) throw new Exception(fileResult.Error.Message);

            note.PublicId = fileResult.PublicId;
            note.Url = fileResult.Url.ToString();
        }

        await context.Notes.AddAsync(note);
        await context.SaveChangesAsync();
        return note;
    }

    public async Task<Note?> DeleteAsync(int id, string username)
    {
        var note = await context.Notes.FindAsync(id);
        if (note == null) return null;

        var member = await subjectGroupRepository.GetGroupMember(username, note.SubjectGroupId);
        if (member == null) throw new Exception("You are not a member");

        context.Notes.Remove(note);
        await context.SaveChangesAsync();

        if (note.PublicId != null)
        {
            var deleteFileResult = await fileUploadService.DeleteFileAsync(note.PublicId);
            if (deleteFileResult.Result != "ok") throw new Exception(deleteFileResult.Error.Message);
        }

        return note;
    }

    public async Task<Note?> GetByIdAsync(int id, string username)
    {
        var note = await context.Notes.FindAsync(id);
        if (note == null) return null;

        var member = await subjectGroupRepository.GetGroupMember(username, note.SubjectGroupId);
        if (member == null) throw new Exception("You are not a member");

        return note;
    }

    public async Task<List<Comment>> GetCommentsAsync(int noteId, string username)
    {
        var note = await GetByIdAsync(noteId, username);
        var comments = await context.Comments.Where(x => x.NoteId == noteId).ToListAsync();
        return comments;
    }

    public async Task<IEnumerable<Note>> GetGroupNotesAsync(int groupId, string username)
    {
        var subjectGroup = await context.SubjectGroups.FindAsync(groupId);
        if (subjectGroup == null) throw new Exception("Subject group doesnt' exist");

        var member = await subjectGroupRepository.GetGroupMember(username, subjectGroup.Id);
        if (member == null) throw new Exception("You are not a member");

        var notes = await context.Notes.Where(x => x.SubjectGroupId == groupId).ToListAsync();

        return notes;
    }

    public async Task<int> SaveChangesAsync()
    {
        var result = await context.SaveChangesAsync();
        return result;
    }
}
