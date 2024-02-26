using DigiDent.InventoryManagement.Domain.Employees.ValueObjects;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Domain.Employees.ReadModels;

public record EmployeeSummary: IFilterable, ISortable
{
    [NotSortable]
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public Position Position { get; init; }
    
    public virtual bool Contains(string value)
    {
        return FullName.Contains(value) ||
               Email.Contains(value) ||
               PhoneNumber.Contains(value) ||
               Position.ToString().Contains(value);
    }

    public IComparable GetDefaultSortProperty() => Position;
}