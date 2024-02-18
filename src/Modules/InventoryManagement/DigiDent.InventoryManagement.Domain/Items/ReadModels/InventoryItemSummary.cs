namespace DigiDent.InventoryManagement.Domain.Items.ReadModels;

public record InventoryItemSummary(
    Guid Id,
    string Name,
    int Quantity,
    string Category,
    int OpenRequestsCount
);