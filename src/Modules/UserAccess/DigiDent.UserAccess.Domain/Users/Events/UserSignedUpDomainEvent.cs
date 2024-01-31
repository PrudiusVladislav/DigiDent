using DigiDent.Shared.Domain.Abstractions;

namespace DigiDent.UserAccess.Domain.Users.Events;

public record UserSignedUpDomainEvent(
    Guid EventId,
    DateTime TimeOfOccurrence,
    User SignedUpUser) : IDomainEvent;