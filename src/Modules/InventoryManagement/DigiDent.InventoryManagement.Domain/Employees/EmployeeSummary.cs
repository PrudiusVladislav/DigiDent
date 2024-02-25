using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Domain.Employees;

public record EmployeeSummary: IFilterable, ISortable
{
    [NotSortable]
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    
    public virtual bool Contains(string value)
    {
        return FullName.Contains(value) ||
               Email.Contains(value) ||
               PhoneNumber.Contains(value) ||
               Role.Contains(value);
    }

    public IComparable DefaultSortProperty => Role;
}