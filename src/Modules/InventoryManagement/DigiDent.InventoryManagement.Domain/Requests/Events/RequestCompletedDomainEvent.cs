using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.InventoryManagement.Domain.Requests.Events;

public record RequestCompletedDomainEvent(
    DateTime TimeOfOccurrence, 
    Request Request) : IDomainEvent;