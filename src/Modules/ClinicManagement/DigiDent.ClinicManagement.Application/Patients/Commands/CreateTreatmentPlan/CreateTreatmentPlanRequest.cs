namespace DigiDent.ClinicManagement.Application.Patients.Commands.CreateTreatmentPlan;

public sealed record CreateTreatmentPlanRequest(
    string DiagnosisDescription,
    DateOnly StartDate);
