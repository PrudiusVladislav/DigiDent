namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetAllDoctors;

public class DoctorDTO
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Specialization { get; init; } = string.Empty;
    public string EmployeeStatus { get; init; } = string.Empty;
}