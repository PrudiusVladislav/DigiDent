namespace DigiDent.ClinicManagement.Application.Patients.Queries.GetPatientProfile;

public class PatientTreatmentPlanDTO
{
    public Guid Id { get; init; }
    public string DiagnosisDescription { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
}