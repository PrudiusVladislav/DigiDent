using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;

namespace DigiDent.ClinicManagement.Domain.Employees.Doctors.ValueObjects;

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