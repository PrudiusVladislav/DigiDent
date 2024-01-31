namespace DigiDent.ClinicManagement.Application.Patients.Queries.GetPatientProfile;

public class PatientPastVisitDTO
{
    public Guid Id { get; init; }
    public Guid DoctorId { get; init; }
    public string DoctorFullName { get; init; } = string.Empty;
    public PatientTreatmentPlanDTO? TreatmentPlan { get; init; }
    public DateTime VisitDateTime { get; init; }
    public string Status { get; init; } = string.Empty;
}