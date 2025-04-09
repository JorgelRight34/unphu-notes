using System;
using api.DTOs.Comment;
using api.Extensions;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public class CommentsController(ICommentRepository commentRepository, IMapper mapper) : ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult<CommentDto>> Create([FromBody] CreateCommentDto request)
    {
        var comment = await commentRepository.CreateAsync(request, User.GetUsername());

        return CreatedAtAction(nameof(GetById), new { id = comment.Id }, mapper.Map<CommentDto>(comment));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CommentDto?>> GetById(int id)
    {
        var comment = await commentRepository.GetByIdAsync(id);
        if (comment == null) return NotFound(new { message = "Comment not found" });

        return Ok(mapper.Map<CommentDto>(comment));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var comment = await commentRepository.DeleteAsync(id);
        if (comment == null) return NotFound(new { message = "Comment not found" });    // Return not found if null

        await commentRepository.DeleteAsync(id);   // Delete the comment
        return NoContent();   // Return no content
    }
}
