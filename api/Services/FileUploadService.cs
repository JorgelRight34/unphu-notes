using System;
using api.Data;
using api.Helpers;
using api.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace api.Services;

public class FileUploadService : IFileUploadService
{
    private readonly Cloudinary _cloudinary;
    public FileUploadService(IOptions<CloudinarySettings> config, ApplicationDbContext context)
    {
        var account = new Account(
            config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret
        );

        _cloudinary = new Cloudinary(account);
    }

    public async Task<ImageUploadResult> AddFileAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();

        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "unphu-notes"
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }

        return uploadResult;
    }

    public async Task<DeletionResult> DeleteFileAsync(string publicId)
    {
        var deleteParams = new DeletionParams($"jp-credit-app/{publicId}");
        var result = await _cloudinary.DestroyAsync(deleteParams);
        return result;
    }
}