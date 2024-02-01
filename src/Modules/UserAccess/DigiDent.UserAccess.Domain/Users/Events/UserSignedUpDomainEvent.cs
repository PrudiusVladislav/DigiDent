using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.UserAccess.Domain.Users.Events;

public record UserSignedUpDomainEvent(
    Guid EventId,
    DateTime TimeOfOccurrence,
    User SignedUpUser) : IDomainEvent;