using DigiDent.Shared.Domain.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Events;

public record PastVisitCreatedDomainEvent(
    Guid EventId,
    DateTime TimeOfOccurrence,
    PastVisit PastVisit): IDomainEvent;