
namespace DigiDent.InventoryManagement.Domain.Actions.ReadModels;

public record ActionSummary(
    int Id,
    string Type,
    int Quantity,
    DateOnly Date,
    Guid EmployeeId,
    string EmployeeName,
    int ItemId,
    string ItemName,
    string ItemCategory
);
