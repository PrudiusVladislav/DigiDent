using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Events;

public record AppointmentClosedDomainEvent(
    Guid EventId,
    DateTime TimeOfOccurrence,
    VisitStatus ClosureStatus,
    Money PricePaid,
    Appointment ClosedAppointment) : IDomainEvent;