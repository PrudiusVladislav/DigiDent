
namespace DigiDent.ClinicManagement.Application.Doctors.Commands.Update;

public sealed record UpdateDoctorRequest(
    string? Gender,
    DateOnly? BirthDate,
    string? Status,
    string? Specialization,
    string? Biography);