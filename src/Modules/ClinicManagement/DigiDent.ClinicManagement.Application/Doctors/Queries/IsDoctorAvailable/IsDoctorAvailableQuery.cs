using DigiDent.Shared.Abstractions.Queries;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Doctors.Queries.IsDoctorAvailable;

public sealed record IsDoctorAvailableQuery(
    Guid DoctorId,
    DateTime DateTime,
    int DurationInMinutes) : IQuery<Result<IsDoctorAvailableResponse>>;