namespace DigiDent.API.Endpoints.ClinicCore.Doctors.DTOs;

public record GetAllDoctorResponseDto(
    string FullName,
    string Email,
    string PhoneNumber,
    string Gender,
    string EmployeeStatus,
    string Specialization);