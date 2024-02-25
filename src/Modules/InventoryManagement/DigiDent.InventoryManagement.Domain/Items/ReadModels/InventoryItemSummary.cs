using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Domain.Items.ReadModels;

public record InventoryItemSummary : IFilterable, ISortable
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public string Category { get; init; } = string.Empty;
    
    public virtual bool Contains(string value)
    {
        return Name.Contains(value) ||
               Quantity.ToString().Contains(value) ||
               Category.Contains(value);
    }

    public IComparable DefaultSortProperty => Id;
}