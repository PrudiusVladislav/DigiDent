using DigiDent.InventoryManagement.Domain.Employees.ReadModels;
using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;

namespace DigiDent.InventoryManagement.Domain.Actions.ReadModels;

public record ActionSummary(
    Guid Id,
    string Type,
    int Quantity,
    DateOnly Date,
    EmployeeSummary ActionPerformer,
    InventoryItemSummary InventoryItem
);
