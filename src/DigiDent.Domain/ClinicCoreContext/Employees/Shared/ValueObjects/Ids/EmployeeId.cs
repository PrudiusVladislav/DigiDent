using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;

public record EmployeeId(Guid Value): IEmployeeId;