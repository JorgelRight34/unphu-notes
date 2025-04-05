using System;
using CloudinaryDotNet.Actions;

namespace api.Interfaces;

public interface IFileUploadService
{
    Task<ImageUploadResult> AddFileAsync(IFormFile file);
    Task<DeletionResult> DeleteFileAsync(string publicId);
}
