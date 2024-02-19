using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;

namespace DigiDent.InventoryManagement.Domain.Items.ReadModels;

public record InventoryItemDetails(
    int Id,
    string Name,
    string Remarks,
    int Quantity,
    string Category,
    List<RequestSummary> OpenRequests,
    List<ActionSummary> LastActions
);
//TODO: probably move to the application layer,
//since the mapping will be from the Domain Entity
//returned form GetById repository method