using System;
using CloudinaryDotNet.Actions;

namespace api.Interfaces;

public interface IFileUploadService
{
    Task<RawUploadResult> AddFileAsync(IFormFile file);
    Task<DeletionResult> DeleteFileAsync(string publicId);
}
