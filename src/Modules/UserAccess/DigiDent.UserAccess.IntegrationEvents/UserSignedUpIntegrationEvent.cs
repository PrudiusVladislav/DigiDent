using DigiDent.Shared.Abstractions.Messaging;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.UserAccess.IntegrationEvents;

public record UserSignedUpIntegrationEvent(
    Guid Id,
    DateTime TimeOfOccurrence,
    FullName FullName,
    Email Email,
    PhoneNumber PhoneNumber,
    Role Role) : IIntegrationEvent;