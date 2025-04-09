using System;
using api.Data;
using api.DTOs.Comment;
using api.Interfaces;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class CommentRepository(ApplicationDbContext context, UserManager<AppUser> userManager, IMapper mapper) : ICommentRepository
{
    public async Task<Comment> CreateAsync(CreateCommentDto commentDto, string username)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user == null) throw new Exception("User not found");

        var note = await context.Notes.FindAsync(commentDto.NoteId);
        if (note == null) throw new Exception("Note not found");

        var comment = mapper.Map<Comment>(commentDto);
        comment.AuthorId = user.Id;
        Console.WriteLine($"comment: {comment.NoteId}");
        Console.WriteLine($"comment: {comment.AuthorId}");
        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment> DeleteAsync(int id)
    {
        var note = await GetByIdAsync(id);
        if (note == null) throw new Exception("Comment not found");

        context.Comments.Remove(note);
        return note;
    }

    public Task<Comment?> GetByIdAsync(int id)
    {
        var note = context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        return note;
    }
}
