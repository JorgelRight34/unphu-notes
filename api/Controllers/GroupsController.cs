using System;
using api.DTOs.Note;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class GroupsController(INoteRepository noteRepository, IMapper mapper) : ApiBaseController
{
    [HttpGet("{id}/notes")]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetSubjectNotes([FromRoute] int id)
    {
        var notes = await noteRepository.GetGroupNotesAsync(id);
        var noteDtos = notes.Select(mapper.Map<NoteDto>);
        return Ok(notes);
    }
}
