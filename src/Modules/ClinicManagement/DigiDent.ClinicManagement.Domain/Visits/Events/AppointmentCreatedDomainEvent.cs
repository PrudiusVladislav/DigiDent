using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Visits.Events;

public record AppointmentCreatedDomainEvent(
    Guid EventId,
    DateTime TimeOfOccurrence,
    Appointment Appointment): IDomainEvent;