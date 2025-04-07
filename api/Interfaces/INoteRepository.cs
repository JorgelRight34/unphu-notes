using System;
using api.DTOs.Note;
using api.Models;

namespace api.Interfaces;

public interface INoteRepository
{
    Task<Note> CreateAsync(CreateNoteDto createNoteDto);
    Task<Note?> GetByIdAsync(int id);
    Task<Note?> DeleteAsync(int id);
    Task<IEnumerable<Note>> GetGroupNotesAsync(int groupId);
}
