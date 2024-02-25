using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Employees.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Domain.Requests.ReadModels;

public record RequestSummary : IFilterable
{
    public Guid Id { get; init; }
    public string Status { get; init; } = string.Empty;
    public int RequestedQuantity { get; init; }
    public DateOnly DateOfRequest { get; init; }
    public string Remarks { get; init; } = string.Empty;
    
    public InventoryItemSummary RequestedItem { get; set; } = null!;
    public EmployeeSummary Requester { get; set; } = null!;
    
    public bool Contains(string searchText)
    {
        return  Status.Contains(searchText) ||
                RequestedItem.Contains(searchText) ||
                RequestedQuantity.ToString().Contains(searchText) ||
                Requester.Contains(searchText) ||
                DateOfRequest.ToString().Contains(searchText) ||
                Remarks.Contains(searchText);
    }
};