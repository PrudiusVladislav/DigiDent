using MediatR;

namespace DigiDent.Shared.Kernel.Abstractions;

/// <summary>
/// Interface that represents a domain event.
/// </summary>
public interface IDomainEvent : INotification
{ 
    DateTime TimeOfOccurrence { get; }
}
