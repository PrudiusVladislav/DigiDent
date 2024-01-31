using DigiDent.ClinicManagement.Domain.Visits.Enumerations;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Visits.Events;

public record AppointmentClosedDomainEvent(
    Guid EventId,
    DateTime TimeOfOccurrence,
    VisitStatus ClosureStatus,
    Money PricePaid,
    Appointment ClosedAppointment) : IDomainEvent;