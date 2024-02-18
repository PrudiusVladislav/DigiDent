using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;

namespace DigiDent.InventoryManagement.Domain.Items.ReadModels;

public record InventoryItemDetails(
    Guid Id,
    string Name,
    string Remarks,
    int Quantity,
    string Category,
    List<RequestSummary> OpenRequests,
    List<ActionSummary> LastActions
);