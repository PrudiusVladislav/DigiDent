using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.InventoryManagement.Domain.Items.ReadModels;

public readonly record struct InventoryItemSummary(
    int Id,
    string Name,
    int Quantity,
    string Category) : IFilterable
{
    public bool Contains(string value)
    {
        return Name.Contains(value) ||
               Quantity.ToString().Contains(value) ||
               Category.Contains(value);
    }
}