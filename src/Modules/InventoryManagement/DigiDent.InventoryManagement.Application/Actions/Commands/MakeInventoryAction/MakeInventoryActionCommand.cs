using DigiDent.InventoryManagement.Domain.Actions.ValueObjects;
using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.Extensions;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.InventoryManagement.Application.Actions.Commands.MakeInventoryAction;

public record MakeInventoryActionCommand: ICommand<Result<int>>
{
    public InventoryItemId ItemId { get; init; }
    public ActionType Type { get; init; }
    public Quantity Quantity { get; init; }
    public EmployeeId PerformerId { get; init; }
    
    internal MakeInventoryActionCommand(
        InventoryItemId itemId,
        ActionType type,
        Quantity quantity,
        EmployeeId performerId)
    {
        ItemId = itemId;
        Type = type;
        Quantity = quantity;
        PerformerId = performerId;
    }

    public static Result<MakeInventoryActionCommand> CreateFromRequest(
        MakeInventoryActionRequest request)
    {
        InventoryItemId itemId = new(request.ItemId);
        Result<ActionType> type = request.ActionType.ToEnum<ActionType>();
        Result<Quantity> quantity = Quantity.Create(request.Quantity);
        EmployeeId performerId = new(request.EmployeeId);

        return Result
            .Merge(type, quantity)
            .Match(onSuccess: () => Result.Ok(
                new MakeInventoryActionCommand(
                    itemId,
                    type.Value,
                    quantity.Value!,
                    performerId)));
    }
    
}