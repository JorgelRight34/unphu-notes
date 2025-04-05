using System;
using api.DTOs.Note;
using api.DTOs.SubjectGroup;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class GroupsController(ISubjectGroupRepository subjectGroupRepository, INoteRepository noteRepository, IMapper mapper) : ApiBaseController
{
    [HttpGet("{id:int}/notes")]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetSubjectNotes([FromRoute] int id)
    {
        var notes = await noteRepository.GetGroupNotesAsync(id);
        var noteDtos = notes.Select(mapper.Map<NoteDto>);
        return Ok(notes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SubjectGroupDto>> GetById([FromRoute] int id)
    {
        var subjectGroup = await subjectGroupRepository.GetByIdAsync(id);
        if (subjectGroup == null) return NotFound();

        return mapper.Map<SubjectGroupDto>(subjectGroup);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var subjectGroup = await subjectGroupRepository.DeleteByIdAsync(id);
        if (subjectGroup == null) return NotFound();

        return NoContent();
    }
}
