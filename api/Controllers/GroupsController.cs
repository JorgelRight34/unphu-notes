using System;
using api.DTOs.Note;
using api.DTOs.SubjectGroup;
using api.Extensions;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class GroupsController(
    ISubjectGroupRepository subjectGroupRepository,
    INoteRepository noteRepository,
    IMapper mapper
) : ApiBaseController
{
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult<IEnumerable<SubjectGroupDto>>> GetAll()
    {
        // Get all subject groups
        var username = User.GetUsername();   // Get the username from the token
        var subjectGroups = await subjectGroupRepository.GetUserSubjectGroups(username);
        if (subjectGroups == null) return Ok(new List<SubjectGroupDto>());

        var subjectGroupDtos = subjectGroups.Select(mapper.Map<SubjectGroupDto>);   // Map them to DTOs
        return Ok(subjectGroupDtos);   // Return mapped model
    }

    [HttpGet("{id:int}/notes")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetSubjectNotes([FromRoute] int id)
    {
        // Prevents the user from accessing other groups notes
        var groupMember = await subjectGroupRepository.GetGroupMember(User.GetUsername(), id);
        if (groupMember == null) return BadRequest("You are not a member of this group.");

        // Get the notes related to the subject with the given id
        var notes = await noteRepository.GetGroupNotesAsync(id);
        var noteDtos = notes.Select(mapper.Map<NoteDto>);   // Map them to DTOs

        return Ok(notes);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<SubjectGroupDto>> GetById([FromRoute] int id)
    {
        // Get a subject group by id
        var subjectGroup = await subjectGroupRepository.GetByIdAsync(id);
        if (subjectGroup == null) return NotFound();    // Return not found if null

        return mapper.Map<SubjectGroupDto>(subjectGroup);   // Return mapped model
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin, Teacher")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var subjectGroup = await subjectGroupRepository.DeleteByIdAsync(id);
        if (subjectGroup == null) return NotFound();

        return NoContent();
    }
}
