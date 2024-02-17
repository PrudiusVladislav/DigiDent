using DigiDent.InventoryManagement.Domain.Items.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.InventoryManagement.Domain.Items.ValueObjects;

public record ItemName
{
    public string Value { get; init; }
    
    internal ItemName(string value)
    {
        Value = value;
    }

    internal static Result<ItemName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail<ItemName>(InventoryItemErrors
                .ItemNameIsEmpty);
        }
        
        return Result.Ok(new ItemName(value));
    }
    
}