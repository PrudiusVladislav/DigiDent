using MediatR;

namespace DigiDent.Shared.Abstractions.Messaging;

/// <summary>
/// Defines an integration event.
/// </summary>
public interface IIntegrationEvent: INotification
{
    Guid Id { get; }
    DateTime TimeOfOccurrence { get; }
}