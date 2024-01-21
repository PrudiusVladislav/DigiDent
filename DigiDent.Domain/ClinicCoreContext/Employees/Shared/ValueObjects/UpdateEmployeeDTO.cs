using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;

public record UpdateEmployeeDTO(
    Gender? Gender= null,
    DateOnly? BirthDate= null,
    EmployeeStatus? Status= null);