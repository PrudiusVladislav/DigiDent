namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetAllDoctors;

public record DoctorDTO(
    Guid Id,
    string FullName,
    string Email,
    string Specialization,
    string EmployeeStatus);