using DigiDent.ClinicManagement.Domain.Patients;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Patients.Commands.CreateTreatmentPlan;

public sealed class CreateTreatmentPlanCommandHandler
    : ICommandHandler<CreateTreatmentPlanCommand, Result<Guid>>
{
    private readonly IPatientsRepository _patientsRepository;

    public CreateTreatmentPlanCommandHandler(IPatientsRepository patientsRepository)
    {
        _patientsRepository = patientsRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateTreatmentPlanCommand command, CancellationToken cancellationToken)
    {
        Patient? patient = await _patientsRepository.GetByIdAsync(
            command.PatientId, cancellationToken);

        if (patient is null)
        {
            return Result.Fail(RepositoryErrors
                .EntityNotFound<Patient>(command.PatientId.Value))
                .MapToType<Guid>();
        }
        
        TreatmentPlan plan = TreatmentPlan.Create(
            command.PlanDetails,
            command.DateOfStart,
            command.PatientId);
        
        patient.TreatmentPlans.Add(plan);
        await _patientsRepository.UpdateAsync(patient, cancellationToken);
        
        return Result.Ok(plan.Id.Value);
    }
}