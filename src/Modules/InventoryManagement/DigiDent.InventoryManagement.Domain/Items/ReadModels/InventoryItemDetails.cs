using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Domain.Items.ReadModels;

public sealed record InventoryItemDetails: InventoryItemSummary
{
    [NotSortable]
    public string Remarks { get; set; } = string.Empty;
    
    public ICollection<RequestSummary> Requests { get; set; }
        = new List<RequestSummary>();
    
    public ICollection<ActionSummary> InventoryActions { get; set; }
        = new List<ActionSummary>();
}