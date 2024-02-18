
using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.InventoryManagement.Domain.Requests.ReadModels;

public record RequestSummary(
    Guid Id,
    int RequestedItemId,
    string RequestedItemName,
    string ItemCategory,
    int RequestedQuantity,
    string Status,
    Guid RequesterId,
    string RequesterName,
    DateOnly Date,
    string Remarks) : IFilterable
{
    public bool Contains(string searchText)
    {
        return RequestedItemName.Contains(searchText) ||
               ItemCategory.Contains(searchText) ||
               RequestedQuantity.ToString().Contains(searchText) ||
               Status.Contains(searchText) ||
               RequesterName.Contains(searchText) ||
               Remarks.Contains(searchText);
    }
};