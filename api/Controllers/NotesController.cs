using System;
using api.DTOs.Comment;
using api.DTOs.File;
using api.DTOs.Note;
using api.Extensions;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public class NotesController(INoteRepository noteRepository, IMapper mapper) : ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult<NoteDto>> Create([FromForm] CreateNoteDto request)
    {
        var note = await noteRepository.CreateAsync(request, User.GetUsername());

        return Ok(mapper.Map<NoteDto>(note));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<NoteDto>> GetById([FromRoute] int id)
    {
        var note = await noteRepository.GetByIdAsync(id, User.GetUsername());
        if (note == null) return NotFound();

        return mapper.Map<NoteDto>(note);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var note = await noteRepository.DeleteAsync(id, User.GetUsername());
        if (note == null) return NotFound();

        return NoContent();
    }

    [HttpGet("{id:int}/comments")]
    public async Task<ActionResult<List<CommentDto>>> GetComments([FromRoute] int id)
    {
        var comments = await noteRepository.GetCommentsAsync(id, User.GetUsername());
        return Ok(mapper.Map<List<CommentDto>>(comments));
    }
}
