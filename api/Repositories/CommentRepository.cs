using System;
using api.Data;
using api.DTOs.Comment;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class CommentRepository(
    ApplicationDbContext context,
    UserManager<AppUser> userManager,
    ISubjectGroupRepository subjectGroupRepository,
    IMapper mapper
) : ICommentRepository
{
    public async Task<Comment> CreateAsync(CreateCommentDto commentDto, string username)
    {
        var note = await context.Notes.FindAsync(commentDto.NoteId);
        if (note == null) throw new Exception("Note not found");

        var member = await subjectGroupRepository.GetGroupMember(username, note.SubjectGroupId);
        if (member == null) throw new Exception("You are not a member");

        var comment = mapper.Map<Comment>(commentDto);
        comment.AuthorId = member.StudentId;
        comment.SubjectGroupId = note.SubjectGroupId;

        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment> DeleteAsync(int id, string username)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user == null) throw new Exception("User not found");

        var comment = await GetByIdAsync(id, username);
        if (comment == null) throw new Exception("Comment not found");

        if (comment.AuthorId != user.Id) throw new Exception("You are not the author of this comment");

        context.Comments.Remove(comment);
        return comment;
    }

    public async Task<Comment?> GetByIdAsync(int id, string username)
    {
        var comment = await context.Comments.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == id);
        if (comment == null) return null;

        var member = await subjectGroupRepository.GetGroupMember(username, comment.SubjectGroupId);
        if (member == null) throw new Exception("You are not a member");

        return comment;
    }
}
