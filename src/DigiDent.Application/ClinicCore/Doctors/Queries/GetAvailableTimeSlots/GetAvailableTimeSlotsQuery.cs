using DigiDent.Application.Shared.Abstractions;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetAvailableTimeSlots;

public sealed record GetAvailableTimeSlotsQuery(
    Guid DoctorId,
    DateTime FromDateTime,
    DateOnly UntilDate,
    int DurationInMinutes) : IQuery<IReadOnlyCollection<DateTime>>;