using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.Doctors.Queries.GetAvailableTimeSlots;

public sealed record GetAvailableTimeSlotsQuery(
    Guid DoctorId,
    DateTime FromDateTime,
    DateOnly UntilDate,
    int DurationInMinutes) : IQuery<IReadOnlyCollection<DateTime>>;