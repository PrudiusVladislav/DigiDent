using MediatR;

namespace DigiDent.Domain.SharedKernel;

/// <summary>
/// Base class for all domain events.
/// </summary>
public abstract record DomainEvent(Guid Id, DateTime TimeOfOccurence): INotification;
