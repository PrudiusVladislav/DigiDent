using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Doctors.ValueObjects;

public record UpdateDoctorDTO(
    Gender? Gender = null,
    DateOnly? BirthDate = null,
    EmployeeStatus? Status = null,
    DoctorSpecialization? Specialization = null,
    string? Biography = null)
{
    public UpdateEmployeeDTO ToUpdateEmployeeDTO
        => new (Gender, BirthDate, Status);
};