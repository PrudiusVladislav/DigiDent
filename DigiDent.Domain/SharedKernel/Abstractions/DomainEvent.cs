using MediatR;

namespace DigiDent.Domain.SharedKernel.Abstractions;

/// <summary>
/// Interface that represents a domain event.
/// </summary>
public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime TimeOfOccurrence { get; }
}
