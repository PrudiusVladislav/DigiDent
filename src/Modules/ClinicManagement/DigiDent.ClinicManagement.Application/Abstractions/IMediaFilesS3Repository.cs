using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace DigiDent.ClinicManagement.Application.Abstractions;

public interface IMediaFilesS3Repository
{
    Task UploadMediaFilesAsync(
        string bucketName,
        string objectKey,
        List<IFormFile> mediaFiles,
        CancellationToken cancellationToken);
    
    Task<GetObjectResponse?> GetMediaFilesAsync(
        string bucketName,
        string objectKey,
        CancellationToken cancellationToken);
}