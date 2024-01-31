using DigiDent.Application.Shared.Abstractions;
using DigiDent.Shared.Domain.ValueObjects;

namespace DigiDent.Application.Shared.IntegrationEvents;

public record UserSignedUpIntegrationEvent(
    Guid Id,
    DateTime TimeOfOccurrence,
    FullName FullName,
    Email Email,
    PhoneNumber PhoneNumber,
    Role Role) : IIntegrationEvent;