using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.InventoryManagement.Domain.Actions.Events;

public record InventoryActionOccurredDomainEvent(
    DateTime TimeOfOccurrence,
    InventoryAction Action): IDomainEvent;