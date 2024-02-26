using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.InventoryManagement.Domain.Items.ValueObjects;

public record InventoryItemId(int Value): ITypedId<int>;