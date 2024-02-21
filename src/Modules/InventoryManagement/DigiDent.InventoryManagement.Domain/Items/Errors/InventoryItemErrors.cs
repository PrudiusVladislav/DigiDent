using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.InventoryManagement.Domain.Items.Errors;

public static class InventoryItemErrors
{
    public static Error QuantityIsNegative => 
        new (
            ErrorType.Validation,
            nameof(Quantity),
            "Quantity can not be less than 0.");
    
    public static Error ItemNameIsEmpty => 
        new (
            ErrorType.Validation,
            nameof(ItemName),
            "Item name can not be empty.");
    
    public static Error ItemNameIsAlreadyTaken(string name) =>
        new (
            ErrorType.Validation,
            nameof(ItemName),
            $"Item name '{name}' is already taken.");
}