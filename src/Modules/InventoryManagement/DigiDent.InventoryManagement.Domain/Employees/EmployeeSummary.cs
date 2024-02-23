using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Domain.Employees;

public class EmployeeSummary: IFilterable
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    
    public bool Contains(string value)
    {
        return FullName.Contains(value);
    }
}