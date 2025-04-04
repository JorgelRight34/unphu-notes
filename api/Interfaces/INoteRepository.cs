using System;
using api.DTOs.Note;

namespace api.Interfaces;

public interface INoteRepository
{
    Task<NoteDto> CreateAsync(CreateNoteDto createNoteDto);
}
