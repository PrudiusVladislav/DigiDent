using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.InventoryManagement.Domain.Shared;

public record EmployeeId(Guid Value): ITypedId<Guid>;