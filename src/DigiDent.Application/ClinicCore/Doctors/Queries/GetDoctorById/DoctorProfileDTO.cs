
namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetDoctorById;

public class DoctorProfileDTO
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Specialization { get; init; } = string.Empty;
    public string EmployeeStatus { get; init; } = string.Empty;
    public string Gender { get; init; } = string.Empty;
    public DateOnly BirthDate { get; init; }
    public string? Biography { get; init; }
}
    