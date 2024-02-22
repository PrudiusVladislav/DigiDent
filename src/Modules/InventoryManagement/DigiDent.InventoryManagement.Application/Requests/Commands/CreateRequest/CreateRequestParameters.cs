namespace DigiDent.InventoryManagement.Application.Requests.Commands.CreateRequest;

public record CreateRequestParameters(
    int RequestedItemId,
    int RequestedQuantity,
    Guid RequesterId,
    string Remarks = "");