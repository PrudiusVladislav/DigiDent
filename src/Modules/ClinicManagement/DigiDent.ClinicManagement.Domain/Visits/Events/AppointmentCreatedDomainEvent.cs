using DigiDent.Shared.Domain.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Events;

public record AppointmentCreatedDomainEvent(
    Guid EventId,
    DateTime TimeOfOccurrence,
    Appointment Appointment): IDomainEvent;