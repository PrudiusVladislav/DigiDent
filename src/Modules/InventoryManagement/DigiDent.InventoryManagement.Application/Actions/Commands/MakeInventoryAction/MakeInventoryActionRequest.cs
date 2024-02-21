namespace DigiDent.InventoryManagement.Application.Actions.Commands.MakeInventoryAction;

public record MakeInventoryActionRequest(
    int ItemId,
    string ActionType,
    int Quantity,
    Guid EmployeeId);