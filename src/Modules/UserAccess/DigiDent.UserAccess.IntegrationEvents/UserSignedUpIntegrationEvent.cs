using DigiDent.Shared.Abstractions.Messaging;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.UserAccess.IntegrationEvents;

public record UserSignedUpIntegrationEvent: IIntegrationEvent
{
    public Guid EventId { get; init; }
    public DateTime TimeOfOccurrence { get; init; }
    public FullName FullName { get; init; } = null!;
    public Email Email { get; init; } = null!;
    public PhoneNumber PhoneNumber { get; init; } = null!;
    public Role Role { get; init; }
}