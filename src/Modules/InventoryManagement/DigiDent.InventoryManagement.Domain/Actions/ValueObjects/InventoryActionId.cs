using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.InventoryManagement.Domain.Actions.ValueObjects;

public record InventoryActionId(int Value): ITypedId<int>;