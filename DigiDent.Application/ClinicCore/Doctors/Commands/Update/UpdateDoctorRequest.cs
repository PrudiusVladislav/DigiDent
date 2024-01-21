
namespace DigiDent.Application.ClinicCore.Doctors.Commands.Update;

public record UpdateDoctorRequest(
    string? Gender,
    DateOnly? BirthDate,
    string? Status,
    string? Specialization,
    string? Biography);