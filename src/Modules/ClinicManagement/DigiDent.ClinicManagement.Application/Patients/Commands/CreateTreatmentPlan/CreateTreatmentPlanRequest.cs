namespace DigiDent.Application.ClinicCore.Patients.Commands.CreateTreatmentPlan;

public sealed record CreateTreatmentPlanRequest(
    string DiagnosisDescription,
    DateOnly StartDate);
