namespace DigiDent.Application.ClinicCore.Patients.Queries.GetAllPatients;

public sealed class PatientDTO
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
}