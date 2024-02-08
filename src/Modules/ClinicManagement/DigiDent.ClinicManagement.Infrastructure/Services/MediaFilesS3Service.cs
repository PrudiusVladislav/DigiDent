using System.IO.Compression;
using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using DigiDent.ClinicManagement.Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace DigiDent.ClinicManagement.Infrastructure.Services;

public class MediaFilesS3Service: IMediaFilesS3Service
{
    private readonly IAmazonS3 _s3Client;

    public MediaFilesS3Service(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    public async Task UploadMediaFilesAsync(
        string bucketName,
        string objectKey,
        List<IFormFile> mediaFiles,
        CancellationToken cancellationToken)
    {
        using MemoryStream memoryStream = new();

        GetObjectRequest getObjectRequest = new()
        {
            BucketName = bucketName,
            Key = objectKey
        };

        try
        {
            GetObjectResponse response = await _s3Client.GetObjectAsync(
                getObjectRequest, cancellationToken);
            
            await response.ResponseStream.CopyToAsync(memoryStream, cancellationToken);
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            memoryStream.SetLength(0);
        }
        
        using (ZipArchive zipArchive = new(memoryStream, ZipArchiveMode.Update, true))
        {
            foreach (var file in mediaFiles)
            {
                var entry = zipArchive.CreateEntry(file.FileName);
                
                await using var entryStream = entry.Open();
                
                await file.CopyToAsync(entryStream, cancellationToken);
            }
        }
        
        memoryStream.Position = 0;
        PutObjectRequest putObjectRequest = new()
        {
            BucketName = bucketName,
            Key = objectKey,
            InputStream = memoryStream
        };

        await _s3Client.PutObjectAsync(putObjectRequest, cancellationToken);
    }
}