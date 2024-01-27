using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.UserAccessContext.Users.Events;

public record UserSignedUpDomainEvent(
    Guid EventId,
    DateTime TimeOfOccurrence,
    User SignedUpUser) : IDomainEvent;