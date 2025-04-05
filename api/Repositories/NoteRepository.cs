using System;
using api.Data;
using api.DTOs.Note;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class NoteRepository(ApplicationDbContext context, IMapper mapper) : INoteRepository
{
    /// <summary>
    /// Creates a new note from the provided data transfer object (DTO).
    /// </summary>
    /// <param name="createNoteDto">The <see cref="CreateNoteDto"/> containing the data for the new note.</param>
    /// <returns>
    /// A <see cref="NoteDto"/> representing the created note.
    /// </returns>
    public async Task<Note> CreateAsync(CreateNoteDto createNoteDto)
    {
        var note = mapper.Map<Note>(createNoteDto);
        await context.Notes.AddAsync(note);
        await context.SaveChangesAsync();
        return note;
    }

    public async Task<IEnumerable<Note>> GetGroupNotesAsync(int groupId)
    {
        var subjectGroup = await context.SubjectGroups.FindAsync(groupId);
        if (subjectGroup == null) throw new Exception("Subject group doesnt' exist");

        var notes = await context.Notes.Where(x => x.SubjectGroupId == groupId).ToListAsync();

        return notes;
    }
}
