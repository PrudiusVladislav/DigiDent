using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.IsDoctorAvailable;

public sealed record IsDoctorAvailableQuery(
    Guid DoctorId,
    DateTime DateTime,
    int DurationInMinutes) : IQuery<Result<IsDoctorAvailableResponse>>;