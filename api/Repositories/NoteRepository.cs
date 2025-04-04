using System;
using api.Data;
using api.DTOs.Note;
using api.Interfaces;
using api.Models;
using AutoMapper;

namespace api.Repositories;

public class NoteRepository(ApplicationDbContext context, IMapper mapper) : INoteRepository
{
    public async Task<NoteDto> CreateAsync(CreateNoteDto createNoteDto)
    {
        var note = mapper.Map<Note>(createNoteDto);
        await context.Notes.AddAsync(note);
        return mapper.Map<NoteDto>(note);
    }
}
