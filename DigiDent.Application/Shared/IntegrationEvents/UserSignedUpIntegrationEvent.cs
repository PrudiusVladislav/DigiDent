using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;
using DigiDent.Domain.UserAccessContext.Users;

namespace DigiDent.Application.Shared.IntegrationEvents;

public record UserSignedUpIntegrationEvent(
    Guid Id,
    DateTime TimeOfOccurrence,
    FullName FullName,
    Email Email,
    PhoneNumber PhoneNumber,
    Role Role) : IIntegrationEvent;