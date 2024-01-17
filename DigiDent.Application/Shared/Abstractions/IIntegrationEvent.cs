using MediatR;

namespace DigiDent.Application.Shared.Abstractions;

public interface IIntegrationEvent: INotification
{
    Guid Id { get; }
    DateTime TimeOfOccurrence { get; }
}