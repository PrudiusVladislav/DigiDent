using MediatR;

namespace DigiDent.Application.Shared.Abstractions;

/// <summary>
/// Defines an integration event.
/// </summary>
public interface IIntegrationEvent: INotification
{
    Guid Id { get; }
    DateTime TimeOfOccurrence { get; }
}