namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;

public abstract record EmployeeId(Guid Value): IEmployeeId<Guid>;