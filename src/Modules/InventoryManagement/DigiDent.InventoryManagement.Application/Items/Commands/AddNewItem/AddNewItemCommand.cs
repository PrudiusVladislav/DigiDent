using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Kernel.Extensions;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.InventoryManagement.Application.Items.Commands.AddNewItem;

public sealed record AddNewItemCommand: ICommand<Result<int>>
{
    public ItemName Name { get; init; } = null!;
    public ItemCategory Category { get; init; }
    public Quantity Quantity { get; init; } = null!;
    public string Remarks { get; init; } = string.Empty;
    
    internal AddNewItemCommand(
        ItemName name,
        ItemCategory category,
        Quantity quantity, 
        string remarks)
    {
        Name = name;
        Category = category;
        Quantity = quantity;
        Remarks = remarks;
    }
    
    public static Result<AddNewItemCommand> CreateFromRequest(
        AddNewItemRequest request)
    {
        Result<ItemName> itemName = ItemName.Create(request.Name);
        Result<ItemCategory> itemCategory = request.Category.ToEnum<ItemCategory>();
        Result<Quantity> quantity = Quantity.Create(request.Quantity);
        
        return Result
            .Merge(itemName, itemCategory, quantity)
            .Match(onSuccess: () => Result.Ok(
                new AddNewItemCommand(
                    itemName.Value!,
                    itemCategory.Value,
                    quantity.Value!, 
                    request.Remarks)));
    }
}