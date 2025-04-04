using System;
using api.DTOs.Note;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public class NotesController(INoteRepository noteRepository) : ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult<NoteDto>> CreateNote([FromBody] CreateNoteDto request)
    {
        var note = await noteRepository.CreateAsync(request);
        return Ok(note);
    }
}
