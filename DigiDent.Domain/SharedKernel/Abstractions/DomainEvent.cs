using MediatR;

namespace DigiDent.Domain.SharedKernel.Abstractions;

/// <summary>
/// Base class for all domain events.
/// </summary>
public abstract record DomainEvent(Guid Id, DateTime TimeOfOccurence)
    : INotification;
