using MediatR;

namespace DigiDent.Shared.Application.Abstractions;

/// <summary>
/// Defines an integration event.
/// </summary>
public interface IIntegrationEvent: INotification
{
    Guid Id { get; }
    DateTime TimeOfOccurrence { get; }
}