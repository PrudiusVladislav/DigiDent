using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.InventoryManagement.Domain.Actions.Events;

public record InventoryActionOccurred(
    DateTime TimeOfOccurrence,
    InventoryAction Action): IDomainEvent;