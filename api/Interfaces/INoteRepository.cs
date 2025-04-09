using System;
using api.DTOs.Note;
using api.Models;

namespace api.Interfaces;

public interface INoteRepository
{
    Task<Note> CreateAsync(CreateNoteDto createNoteDto, string username);
    Task<Note?> GetByIdAsync(int id, string username);
    Task<Note?> DeleteAsync(int id, string username);
    Task<IEnumerable<Note>> GetGroupNotesAsync(int groupId, string username);
    Task<List<Comment>> GetCommentsAsync(int noteId, string username);
    Task<int> SaveChangesAsync();
}
