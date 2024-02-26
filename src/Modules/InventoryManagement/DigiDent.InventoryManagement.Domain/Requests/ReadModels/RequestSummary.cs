using DigiDent.InventoryManagement.Domain.Employees.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Requests.ValueObjects;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Domain.Requests.ReadModels;

public record RequestSummary : IFilterable
{
    [NotSortable]
    public Guid Id { get; init; }
    public RequestStatus Status { get; init; }
    public int RequestedQuantity { get; init; }
    public DateOnly DateOfRequest { get; init; }
    [NotSortable]
    public string Remarks { get; init; } = string.Empty;
    
    public InventoryItemSummary RequestedItem { get; set; } = null!;
    public EmployeeSummary Requester { get; set; } = null!;
    
    public bool Contains(string searchText)
    {
        return  Status.ToString().Contains(searchText) ||
                RequestedItem.Contains(searchText) ||
                RequestedQuantity.ToString().Contains(searchText) ||
                Requester.Contains(searchText) ||
                DateOfRequest.ToString().Contains(searchText) ||
                Remarks.Contains(searchText);
    }
};