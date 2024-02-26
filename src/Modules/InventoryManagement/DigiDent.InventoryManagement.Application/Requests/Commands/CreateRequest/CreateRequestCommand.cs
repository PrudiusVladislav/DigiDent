using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Employees.ValueObjects;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.InventoryManagement.Application.Requests.Commands.CreateRequest;

public record CreateRequestCommand: ICommand<Result<Guid>>
{
    public InventoryItemId RequestedItemId { get; init; } = null!;
    public Quantity RequestedQuantity { get; init; } = null!;
    public EmployeeId RequesterId { get; init; } = null!;
    public string Remarks { get; init; } = string.Empty;

    private CreateRequestCommand(
        InventoryItemId requestedItemId,
        Quantity requestedQuantity,
        EmployeeId requesterId,
        string remarks)
    {
        RequestedItemId = requestedItemId;
        RequestedQuantity = requestedQuantity;
        RequesterId = requesterId;
        Remarks = remarks;
    }

    public static Result<CreateRequestCommand> CreateFromRequest(
        CreateRequestParameters parameters)
    {
        InventoryItemId requestedItemId = new(parameters.RequestedItemId);
        Result<Quantity> quantity = Quantity.Create(parameters.RequestedQuantity);
        EmployeeId requesterId = new(parameters.RequesterId);
        string remarks = parameters.Remarks;

        return quantity.Match(onSuccess: () => Result.Ok(
            new CreateRequestCommand(
                requestedItemId,
                quantity.Value!,
                requesterId,
                remarks)));
    }
}