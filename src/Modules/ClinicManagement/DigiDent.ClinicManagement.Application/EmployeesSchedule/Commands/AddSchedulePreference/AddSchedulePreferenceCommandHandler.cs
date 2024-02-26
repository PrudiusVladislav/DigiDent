using DigiDent.ClinicManagement.Domain.Employees.Shared;
using DigiDent.ClinicManagement.Domain.Employees.Shared.Abstractions;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.EmployeesSchedule.Commands.AddSchedulePreference;

public sealed class AddSchedulePreferenceCommandHandler
    : ICommandHandler<AddSchedulePreferenceCommand, Result>
{
    private readonly IAllEmployeesRepository _allEmployeesRepository;

    public AddSchedulePreferenceCommandHandler(
        IAllEmployeesRepository allEmployeesRepository)
    {
        _allEmployeesRepository = allEmployeesRepository;
    }

    public async Task<Result> Handle(
        AddSchedulePreferenceCommand command, CancellationToken cancellationToken)
    {
        Employee? employee = await _allEmployeesRepository.GetByIdAsync(
            command.EmployeeId,
            includeScheduling: true,
            cancellationToken: cancellationToken);
        
        if (employee is null)
            return Result.Fail(RepositoryErrors
                .EntityNotFound<Employee, Guid>(command.EmployeeId));
        
        Result<SchedulePreference> schedulePreferenceResult = SchedulePreference.Create(
            command.Date,
            command.EmployeeId,
            command.StartEndTime,
            command.IsSetAsDayOff);
        
        if (schedulePreferenceResult.IsFailure) 
            return schedulePreferenceResult;
        
        Result additionResult = employee.AddSchedulePreference(
            schedulePreferenceResult.Value!);
        
        if (additionResult.IsFailure)
            return additionResult;
        
        await _allEmployeesRepository.UpdateAsync(employee, cancellationToken);
        return Result.Ok();
    }
}