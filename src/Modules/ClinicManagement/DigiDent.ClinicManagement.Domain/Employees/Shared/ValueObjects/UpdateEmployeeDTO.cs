using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;

namespace DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;

public record UpdateEmployeeDTO(
    Gender? Gender= null,
    DateOnly? BirthDate= null,
    EmployeeStatus? Status= null);