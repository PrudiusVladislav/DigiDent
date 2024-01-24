using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Patients.Commands.CreateTreatmentPlan;

public sealed record CreateTreatmentPlanCommand : ICommand<Result<Guid>>
{
    public PatientId PatientId { get; init; } = null!;
    public TreatmentPlanDetails PlanDetails { get; init; } = null!;
    public DateOnly DateOfStart { get; init; }

    public static Result<CreateTreatmentPlanCommand> CreateFromRequest(
        CreateTreatmentPlanRequest request, Guid patientId)
    {
        var detailsResult = TreatmentPlanDetails.Create(
            request.DiagnosisDescription);

        if (detailsResult.IsFailure)
            return detailsResult.MapToType<CreateTreatmentPlanCommand>();
        
        return Result.Ok(new CreateTreatmentPlanCommand
        {
            PatientId = new PatientId(patientId),
            PlanDetails = detailsResult.Value!,
            DateOfStart = request.StartDate
        });
    }
}
