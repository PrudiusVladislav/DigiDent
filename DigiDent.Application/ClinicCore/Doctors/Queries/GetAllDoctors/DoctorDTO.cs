namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetAllDoctors;

public record DoctorDTO(
    Guid Id,
    string FullName,
    string Email,
    string PhoneNumber,
    string Specialization,
    string EmployeeStatus);