using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Doctors.ValueObjects;

public record DoctorId(Guid Value) : EmployeeId(Value);