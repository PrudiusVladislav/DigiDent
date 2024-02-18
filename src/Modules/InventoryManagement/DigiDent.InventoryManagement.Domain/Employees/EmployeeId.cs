using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.InventoryManagement.Domain.Employees;

public record EmployeeId(Guid Value): ITypedId<Guid>;