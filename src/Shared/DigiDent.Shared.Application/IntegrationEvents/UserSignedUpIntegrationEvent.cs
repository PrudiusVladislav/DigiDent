using DigiDent.Shared.Application.Abstractions;
using DigiDent.Shared.Domain.ValueObjects;

namespace DigiDent.Shared.Application.IntegrationEvents;

public record UserSignedUpIntegrationEvent(
    Guid Id,
    DateTime TimeOfOccurrence,
    FullName FullName,
    Email Email,
    PhoneNumber PhoneNumber,
    Role Role) : IIntegrationEvent;