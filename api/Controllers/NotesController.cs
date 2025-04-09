using System;
using api.DTOs.File;
using api.DTOs.Note;
using api.Extensions;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public class NotesController(INoteRepository noteRepository, IFileUploadService fileUploadService, IMapper mapper) : ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult<NoteDto>> Create([FromForm] CreateNoteDto request)
    {   
        var username = User.GetUsername();
        var note = await noteRepository.CreateAsync(request, username);

        return Ok(mapper.Map<NoteDto>(note));
    }

    [HttpDelete("{id:int}/delete-file")]
    public async Task<ActionResult> DeleteFile([FromRoute] int id)
    {
        var note = await noteRepository.GetByIdAsync(id);
        if (note == null) return NotFound("Note not found");

        if (note.PublicId == null) return BadRequest("No file to delete");

        var result = await fileUploadService.DeleteFileAsync(note.PublicId);
        if (result.Error != null) return BadRequest(result.Error.Message);

        note.PublicId = null;
        note.Url = null;
        await noteRepository.SaveChangesAsync();

        return NoContent();
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
