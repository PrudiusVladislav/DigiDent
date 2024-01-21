using DigiDent.Application.Shared.Abstractions;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.IsDoctorAvailable;

public record IsDoctorAvailableQuery(
    Guid DoctorId,
    DateTime DateTime,
    int DurationInMinutes) : IQuery<IsDoctorAvailableResponse>;