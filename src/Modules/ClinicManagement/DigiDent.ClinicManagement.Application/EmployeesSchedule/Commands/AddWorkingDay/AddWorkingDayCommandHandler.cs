using DigiDent.ClinicManagement.Domain.Employees.Shared;
using DigiDent.ClinicManagement.Domain.Employees.Shared.Abstractions;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.EmployeesSchedule.Commands.AddWorkingDay;

public sealed class AddWorkingDayCommandHandler
    : ICommandHandler<AddWorkingDayCommand, Result>
{
    private readonly IAllEmployeesRepository _allEmployeesRepository;

    public AddWorkingDayCommandHandler(
        IAllEmployeesRepository allEmployeesRepository)
    {
        _allEmployeesRepository = allEmployeesRepository;
    }

    public async Task<Result> Handle(
        AddWorkingDayCommand request, CancellationToken cancellationToken)
    {
        Employee? employee = await _allEmployeesRepository.GetByIdAsync(
            request.EmployeeId,
            includeScheduling: true,
            cancellationToken: cancellationToken);
        
        if (employee is null)
            return Result.Fail(RepositoryErrors
                .EntityNotFound<Employee, Guid>(request.EmployeeId));
        
        Result<WorkingDay> workingDayResult = WorkingDay.Create(
            request.Date,
            request.StartEndTime,
            request.EmployeeId);

        if (workingDayResult.IsFailure)
            return workingDayResult;
        
        Result additionResult = employee.AddWorkingDay(workingDayResult.Value!);
        
        if (additionResult.IsFailure) 
            return additionResult;
        
        await _allEmployeesRepository.UpdateAsync(employee, cancellationToken);
        return Result.Ok();
    }
}