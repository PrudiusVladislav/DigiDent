using Microsoft.AspNetCore.Http;

namespace DigiDent.ClinicManagement.Application.Abstractions;

public interface IMediaFilesS3Service
{
    Task UploadMediaFilesAsync(
        string bucketName,
        string objectKey,
        List<IFormFile> mediaFiles,
        CancellationToken cancellationToken);
}