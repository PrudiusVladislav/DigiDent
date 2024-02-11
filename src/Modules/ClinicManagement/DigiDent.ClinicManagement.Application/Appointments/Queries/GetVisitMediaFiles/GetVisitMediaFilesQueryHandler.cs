using Amazon.S3.Model;
using DigiDent.ClinicManagement.Application.Abstractions;
using DigiDent.ClinicManagement.Application.Constants;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.Appointments.Queries.GetVisitMediaFiles;

public sealed class GetVisitMediaFilesQueryHandler
    : IQueryHandler<GetVisitMediaFilesQuery, GetObjectResponse?>
{
    private readonly IMediaFilesS3Repository _mediaFilesS3Repository;

    public GetVisitMediaFilesQueryHandler(
        IMediaFilesS3Repository mediaFilesS3Repository)
    {
        _mediaFilesS3Repository = mediaFilesS3Repository;
    }

    public async Task<GetObjectResponse?> Handle(
        GetVisitMediaFilesQuery query, CancellationToken cancellationToken)
    {
        return await _mediaFilesS3Repository.GetMediaFilesAsync(
            S3Keys.BucketName, 
            S3Keys.VisitAttachedMediaKey(new AppointmentId(query.VisitId)),
            cancellationToken);
    }
}