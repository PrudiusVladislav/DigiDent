using DigiDent.InventoryManagement.Domain.Items.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.InventoryManagement.Domain.Items.ValueObjects;

public record Quantity
{
    public int Value { get; private set; }

    internal Quantity(int value)
    {
        Value = value;
    }
    
    public static Result<Quantity> Create(int value)
    {
        if (IsNegative(value))
        {
            return Result.Fail<Quantity>(InventoryItemErrors
                .QuantityIsNegative);
        }
        
        return Result.Ok(new Quantity(value));
    }
    
    public Result Add(Quantity quantity)
    {
        Value += quantity.Value;
        return Result.Ok();
    }
    
    public Result Subtract(Quantity quantity)
    {
        if (IsNegative(Value - quantity.Value))
        {
            return Result.Fail(InventoryItemErrors
                .QuantityIsNegative);
        }
        
        Value -= quantity.Value;
        return Result.Ok();
    }
    
    private static bool IsNegative(int value) => value < 0;
}