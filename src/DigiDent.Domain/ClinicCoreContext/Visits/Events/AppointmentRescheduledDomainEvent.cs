using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Events;

public record AppointmentRescheduledDomainEvent(
    Guid EventId,
    DateTime TimeOfOccurrence,
    Appointment Appointment): IDomainEvent;