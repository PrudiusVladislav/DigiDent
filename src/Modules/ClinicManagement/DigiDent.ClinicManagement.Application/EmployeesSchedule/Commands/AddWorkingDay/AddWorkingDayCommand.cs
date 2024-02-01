using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.EmployeesSchedule.Commands.AddWorkingDay;

public sealed class AddWorkingDayCommand: ICommand<Result>
{
    public EmployeeId EmployeeId { get; init; } = null!;
    public DateOnly Date { get; init; }
    public StartEndTime StartEndTime { get; init; } = null!;
    
    public static Result<AddWorkingDayCommand> CreateFromRequest(
        Guid employeeId, AddWorkingDayRequest request)
    {
        var employeeIdTyped = new EmployeeId(employeeId);
        
        var dateResult = new Result();
        if (request.Date <= DateOnly.FromDateTime(DateTime.Now))
            dateResult.AddError(WorkingDayDateIsInThePast);
        
        var timeResult = StartEndTime.Create(request.StartTime, request.EndTime);
        
        var mergedResult = Result.Merge(dateResult, timeResult);
        if (mergedResult.IsFailure)
            return mergedResult.MapToType<AddWorkingDayCommand>();

        return Result.Ok(new AddWorkingDayCommand
        {
            EmployeeId = employeeIdTyped,
            Date = request.Date,
            StartEndTime = timeResult.Value!
        });
    }
    
    private static Error WorkingDayDateIsInThePast
        => new Error(
            ErrorType.Validation,
            nameof(AddWorkingDayCommand),
            "Date of a new working day must be in the future");
}