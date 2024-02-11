using DigiDent.ClinicManagement.Domain.Visits.Abstractions;

namespace DigiDent.ClinicManagement.Application.Constants;

public class S3Keys
{
    public const string BucketName = "digident-media-attachments";
    
    public static string VisitAttachedMediaKey(IVisitId<Guid> visitId) =>
        $"visits-files/zipped/{visitId.Value}";
}