
using DigiDent.Shared.Kernel.Abstractions;

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
    string ItemCategory) : IFilterable
{
    public bool Contains(string value)
    {
        return Type.Contains(value) ||
               Quantity.ToString().Contains(value) ||
               EmployeeName.Contains(value) ||
               ItemName.Contains(value) ||
               ItemCategory.Contains(value);
    }
}
