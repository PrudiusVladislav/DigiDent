namespace DigiDent.InventoryManagement.Application.Items.Commands.AddNewItem;

public record AddNewItemRequest(
    string Name,
    string Category,
    int Quantity,
    string Remarks = "");