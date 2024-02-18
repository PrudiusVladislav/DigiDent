using DigiDent.InventoryManagement.Domain.Employees.ReadModels;
using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;

namespace DigiDent.InventoryManagement.Domain.Requests.ReadModels;

public record RequestSummary(
    Guid Id,
    InventoryItemSummary Item,
    int Quantity,
    string Status,
    EmployeeSummary Requester,
    DateOnly Date,
    string Remarks);