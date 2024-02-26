using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.InventoryManagement.Domain.Employees.ValueObjects;

public record EmployeeId(Guid Value): ITypedId<Guid>;