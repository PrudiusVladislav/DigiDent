using DigiDent.Shared.Domain.Abstractions;

namespace DigiDent.Domain.UserAccessContext.Users.Events;

public record UserSignedUpDomainEvent(
    Guid EventId,
    DateTime TimeOfOccurrence,
    User SignedUpUser) : IDomainEvent;