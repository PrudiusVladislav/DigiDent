using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;

public record WorkingDayId(Guid Value): ITypedId<Guid>;