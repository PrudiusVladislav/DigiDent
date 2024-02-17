using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Visits.Events;

public record AppointmentRescheduledDomainEvent(
    DateTime TimeOfOccurrence,
    Appointment Appointment): IDomainEvent;