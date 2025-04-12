using System;
using api.Data;
using api.DTOs.Note;
using api.Interfaces;
using api.Models;
using AutoMapper;
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

        var entry = await context.Notes.AddAsync(note);
        await context.SaveChangesAsync();

        if (createNoteDto.Files != null && createNoteDto.Files.Count > 0)
        {
            foreach (var file in createNoteDto.Files)
            {
                try
                {
                    var fileUpload = await GenerateNoteFile(file, entry.Entity.Id);
                    context.NoteFiles.Add(fileUpload);
                }
                catch
                {
                    context.Remove(entry.Entity);
                    await context.SaveChangesAsync();
                }
            }
        }

        await context.SaveChangesAsync();

        return note;
    }

    public async Task<NoteFile> GenerateNoteFile(IFormFile file, int noteId)
    {
        var fileResult = await fileUploadService.AddFileAsync(file);
        if (fileResult.Error != null) throw new Exception(fileResult.Error.Message);

        var fileUpload = new NoteFile
        {
            PublicId = fileResult.PublicId,
            Url = fileResult.Url.ToString(),
            NoteId = noteId
        };

        return fileUpload;
    }

    public async Task<Note?> DeleteAsync(int id, string username)
    {
        var note = await context.Notes.Include(x => x.NoteFiles).FirstOrDefaultAsync(x => x.Id == id);
        if (note == null) return null;

        var member = await subjectGroupRepository.GetGroupMember(username, note.SubjectGroupId);
        if (member == null) throw new Exception("You are not a member");

        foreach (var file in note.NoteFiles)
        {
            var result = await fileUploadService.DeleteFileAsync(file.PublicId);
            if (result.Error == null) context.NoteFiles.Remove(file);
        }

        var comments = await context.Comments.Where(x => x.NoteId == id).ToListAsync();

        context.Comments.RemoveRange(comments);
        context.Notes.Remove(note);
        await context.SaveChangesAsync();

        return note;
    }

    public async Task<Note?> GetByIdAsync(int id, string username)
    {
        var note = await context.Notes.Include(x => x.NoteFiles).FirstOrDefaultAsync(x => x.Id == id);
        if (note == null) return null;

        var member = await subjectGroupRepository.GetGroupMember(username, note.SubjectGroupId);
        if (member == null) throw new Exception("You are not a member");

        return note;
    }

    public async Task<List<Comment>> GetCommentsAsync(int noteId, string username)
    {
        var note = await GetByIdAsync(noteId, username);
        var comments = await context.Comments
            .Where(x => x.NoteId == noteId)
            .Include(x => x.Author)
            .ToListAsync();
        return comments;
    }

    public async Task<int> SaveChangesAsync()
    {
        var result = await context.SaveChangesAsync();
        return result;
    }
}
