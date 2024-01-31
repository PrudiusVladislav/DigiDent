namespace DigiDent.ClinicManagement.Application.Patients.Queries.GetAllPatients;

public class PatientDTO
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
}