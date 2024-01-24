namespace DigiDent.Application.ClinicCore.Patients.Queries.GetPatientProfile;

public sealed class PatientTreatmentPlanDTO
{
    public Guid Id { get; init; }
    public string DiagnosisDescription { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
}