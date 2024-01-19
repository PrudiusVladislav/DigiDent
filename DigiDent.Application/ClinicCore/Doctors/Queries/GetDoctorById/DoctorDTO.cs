
namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetDoctorById;

public record DoctorDTO(
    Guid Id,
    string FullName,
    string Email,
    string PhoneNumber,
    string Specialization,
    string EmployeeStatus,
    string Gender,
    DateOnly BirthDate,
    string? Biography);