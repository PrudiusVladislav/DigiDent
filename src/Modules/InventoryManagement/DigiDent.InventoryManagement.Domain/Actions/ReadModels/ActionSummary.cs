using DigiDent.InventoryManagement.Domain.Employees.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Domain.Actions.ReadModels;

public record ActionSummary : IFilterable
{
    public int Id { get; init; }
    public string Type { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public DateOnly Date { get; init; }
    public EmployeeSummary ActionPerformer { get; set; } = null!;
    public InventoryItemSummary InventoryItem { get; set; } = null!;
    
    public bool Contains(string value)
    {
        return Type.Contains(value) ||
               Quantity.ToString().Contains(value) || 
               Date.ToString().Contains(value) ||
               ActionPerformer.Contains(value) ||
               InventoryItem.Contains(value);
    }
}
