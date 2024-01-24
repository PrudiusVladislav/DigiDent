using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.EmployeesSchedule.Commands.AddSchedulePreference;

public class AddSchedulePreferenceCommandHandler
    : ICommandHandler<AddSchedulePreferenceCommand, Result>
{
    private readonly IAllEmployeesRepository _allEmployeesRepository;

    public AddSchedulePreferenceCommandHandler(IAllEmployeesRepository allEmployeesRepository)
    {
        _allEmployeesRepository = allEmployeesRepository;
    }

    public async Task<Result> Handle(
        AddSchedulePreferenceCommand request, CancellationToken cancellationToken)
    {
        var employee = await _allEmployeesRepository.GetByIdAsync(
            request.EmployeeId,
            includeScheduling: true,
            cancellationToken: cancellationToken);
        
        if (employee is null)
            return Result.Fail(RepositoryErrors
                .EntityNotFound<Employee>(request.EmployeeId.Value));
        
        var schedulePreferenceResult = SchedulePreference.Create(
            request.Date,
            request.EmployeeId,
            request.StartEndTime,
            request.IsSetAsDayOff);
        
        if (schedulePreferenceResult.IsFailure) 
            return schedulePreferenceResult;
        
        var additionResult = employee.AddSchedulePreference(
            schedulePreferenceResult.Value!);
        
        if (additionResult.IsFailure)
            return additionResult;
        
        await _allEmployeesRepository.UpdateAsync(employee, cancellationToken);
        return Result.Ok();
    }
}