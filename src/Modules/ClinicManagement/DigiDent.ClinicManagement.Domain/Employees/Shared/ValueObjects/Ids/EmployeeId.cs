using DigiDent.ClinicManagement.Domain.Employees.Shared.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;

public record EmployeeId(Guid Value): IEmployeeId;