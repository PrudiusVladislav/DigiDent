using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Patients.Commands.CreateTreatmentPlan;

public sealed record CreateTreatmentPlanCommand : 
    ICommand<Result<Guid>>
{
    public PatientId PatientId { get; init; } = null!;
    public TreatmentPlanDetails PlanDetails { get; init; } = null!;
    public DateOnly DateOfStart { get; init; }

    public static Result<CreateTreatmentPlanCommand> CreateFromRequest(
        CreateTreatmentPlanRequest request, Guid patientId)
    {
        Result<TreatmentPlanDetails> detailsResult = TreatmentPlanDetails.Create(
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
