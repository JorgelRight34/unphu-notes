using System;
using api.DTOs.Note;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public class NotesController(INoteRepository noteRepository, IMapper mapper) : ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult<NoteDto>> Create([FromBody] CreateNoteDto request)
    {
        var note = await noteRepository.CreateAsync(request);
        return Ok(mapper.Map<NoteDto>(note));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<NoteDto>> GetById([FromRoute] int id)
    {
        var note = await noteRepository.GetByIdAsync(id);
        if (note == null) return NotFound();

        return mapper.Map<NoteDto>(note);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var note = await noteRepository.DeleteAsync(id);
        if (note == null) return NotFound();

        return NoContent();
    }
}
