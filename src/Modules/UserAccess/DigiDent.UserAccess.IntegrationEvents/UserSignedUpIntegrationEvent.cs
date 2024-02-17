using DigiDent.Shared.Abstractions.Messaging;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.UserAccess.IntegrationEvents;

public record UserSignedUpIntegrationEvent: IIntegrationEvent
{
    public DateTime TimeOfOccurrence { get; init; }
    public string UserFullName { get; init; } = null!;
    public string UserEmail { get; init; } = null!;
    public Guid UserId { get; init; }
}