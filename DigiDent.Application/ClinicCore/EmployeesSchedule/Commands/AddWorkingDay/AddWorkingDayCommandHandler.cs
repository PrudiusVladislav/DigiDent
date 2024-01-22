using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.EmployeesSchedule.Commands.AddWorkingDay;

public class AddWorkingDayCommandHandler
    : ICommandHandler<AddWorkingDayCommand, Result>
{
    private readonly IAllEmployeesRepository _allEmployeesRepository;

    public AddWorkingDayCommandHandler(IAllEmployeesRepository allEmployeesRepository)
    {
        _allEmployeesRepository = allEmployeesRepository;
    }

    public async Task<Result> Handle(
        AddWorkingDayCommand request, CancellationToken cancellationToken)
    {
        var employee = await _allEmployeesRepository.GetByIdAsync(
            request.EmployeeId,
            includeScheduling: true,
            cancellationToken: cancellationToken);
        
        if (employee is null)
            return Result.Fail(RepositoryErrors
                .EntityNotFound<Employee>(request.EmployeeId.Value));
        
        var workingDayResult = WorkingDay.Create(
            request.Date,
            request.StartEndTime,
            request.EmployeeId);

        if (workingDayResult.IsFailure)
            return workingDayResult;
        
        var additionResult = employee.AddWorkingDay(workingDayResult.Value!);
        
        if (additionResult.IsFailure) return additionResult;
        
        await _allEmployeesRepository.UpdateAsync(employee, cancellationToken);
        return Result.Ok();
    }
}