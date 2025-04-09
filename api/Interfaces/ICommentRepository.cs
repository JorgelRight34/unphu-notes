using System;
using api.DTOs.Comment;
using api.Models;

namespace api.Interfaces;

public interface ICommentRepository
{
    public Task<Comment> CreateAsync(CreateCommentDto commentDto, string username);
    public Task<Comment?> GetByIdAsync(int id, string username);
    public Task<Comment> DeleteAsync(int id, string username);
}
