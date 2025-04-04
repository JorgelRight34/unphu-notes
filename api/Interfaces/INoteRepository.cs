using System;
using api.DTOs.Note;
using api.Models;

namespace api.Interfaces;

public interface INoteRepository
{
    Task<Note> CreateAsync(CreateNoteDto createNoteDto);
    Task<IEnumerable<Note>> GetGroupNotesAsync(int groupId);
}
