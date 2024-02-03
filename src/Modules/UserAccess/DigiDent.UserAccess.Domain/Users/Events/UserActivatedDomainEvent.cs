using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.UserAccess.Domain.Users.Events;

public record UserActivatedDomainEvent(
    Guid EventId,
    DateTime TimeOfOccurrence,
    User ActivatedUser): IDomainEvent;