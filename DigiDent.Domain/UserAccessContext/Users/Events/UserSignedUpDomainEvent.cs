using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.UserAccessContext.Users.Events;

public record UserSignedUpDomainEvent(
    Guid Id,
    DateTime TimeOfOccurrence,
    User User) : IDomainEvent;