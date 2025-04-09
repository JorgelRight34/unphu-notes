using System;
using api.Data;
using api.DTOs.Note;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

<<<<<<< HEAD
public class NoteRepository(ApplicationDbContext context, IFileUploadService fileUploadService, UserManager<AppUser> userManager, IMapper mapper) : INoteRepository
=======
public class NoteRepository(ApplicationDbContext context, IFileUploadService fileUploadService, IMapper mapper, UserManager<AppUser> userManager) : INoteRepository
>>>>>>> 7922e999fc581cc075ea25dc008a00fa625c4763
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
        var user = await userManager.FindByNameAsync(username);
        if (user == null) throw new Exception("User doesn't exist");

        var note = mapper.Map<Note>(createNoteDto);
        note.StudentId = user.Id;

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

    public async Task<Note?> DeleteAsync(int id)
    {
        var note = await context.Notes.FindAsync(id);
        if (note == null) return null;

        context.Notes.Remove(note);
        await context.SaveChangesAsync();

        if (note.PublicId != null)
        {
            var deleteFileResult = await fileUploadService.DeleteFileAsync(note.PublicId);
            if (deleteFileResult.Result != "ok") throw new Exception(deleteFileResult.Error.Message);
        }

        return note;
    }

    public async Task<Note?> GetByIdAsync(int id)
    {
        var note = await context.Notes.FindAsync(id);
        return note;
    }

    public async Task<List<Comment>> GetCommentsAsync(int noteId)
    {
        var comments = await context.Comments.Where(x => x.NoteId == noteId).ToListAsync();
        return comments;
    }

    public async Task<IEnumerable<Note>> GetGroupNotesAsync(int groupId)
    {
        var subjectGroup = await context.SubjectGroups.FindAsync(groupId);
        if (subjectGroup == null) throw new Exception("Subject group doesnt' exist");

        var notes = await context.Notes.Where(x => x.SubjectGroupId == groupId).ToListAsync();

        return notes;
    }

    public async Task<int> SaveChangesAsync()
    {
        var result = await context.SaveChangesAsync();
        return result;
    }
}
