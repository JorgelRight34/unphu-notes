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

    /// <summary>
    /// Uploads a file to Cloudinary and returns the upload result.
    /// </summary>
    /// <param name="file"></param>
    /// <exception cref="Exception">Thrown if the file upload fails.</exception>
    /// <remarks>Uses the Cloudinary API to upload the file.</remarks>
    /// <returns>UploadResult object containing the result of the upload.</returns>
    public async Task<RawUploadResult> AddFileAsync(IFormFile file)
    {
        var uploadResult = new RawUploadResult();

        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "unphu-notes",
                PublicId = Path.GetFileNameWithoutExtension(file.FileName),
                Overwrite = true
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }

        return uploadResult;
    }

    /// <summary>
    /// Deletes a file from Cloudinary using its public ID.
    /// /// </summary>
    /// <param name="publicId">The public ID of the file to delete.</param>
    /// <returns>A task representing the asynchronous operation, with a result of type DeletionResult.</returns>
    /// <exception cref="Exception">Thrown if the file deletion fails.</exception>
    /// <remarks>Uses the public ID to identify the file in Cloudinary.</remarks>
    /// <returns>DeletionResult object containing the result of the deletion.</returns>
    public async Task<DeletionResult> DeleteFileAsync(string publicId)
    {
        var deleteParams = new DeletionParams($"jp-credit-app/{publicId}");
        var result = await _cloudinary.DestroyAsync(deleteParams);
        return result;
    }
}