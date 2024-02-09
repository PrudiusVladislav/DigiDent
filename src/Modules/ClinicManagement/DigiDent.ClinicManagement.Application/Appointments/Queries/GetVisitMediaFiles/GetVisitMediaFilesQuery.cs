using Amazon.S3.Model;
using DigiDent.Shared.Abstractions.Queries;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Appointments.Queries.GetVisitMediaFiles;

public sealed record GetVisitMediaFilesQuery(Guid VisitId)
    : IQuery<GetObjectResponse?>;