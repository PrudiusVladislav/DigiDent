﻿using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Patients.Commands.CreateTreatmentPlan;

public class CreateTreatmentPlanCommandHandler
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