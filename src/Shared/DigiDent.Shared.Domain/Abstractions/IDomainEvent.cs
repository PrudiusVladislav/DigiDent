using MediatR;

namespace DigiDent.Shared.Domain.Abstractions;

/// <summary>
/// Interface that represents a domain event.
/// </summary>
public interface IDomainEvent : INotification
{
    Guid EventId { get; }
    DateTime TimeOfOccurrence { get; }
}
