using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.EmployeesSchedule.Commands.AddSchedulePreference;

public sealed record AddSchedulePreferenceCommand : ICommand<Result>
{
    public EmployeeId EmployeeId { get; init; } = null!;
    public DateOnly Date { get; init; }
    public StartEndTime? StartEndTime { get; init; }
    public bool IsSetAsDayOff { get; init; }
    
    public static Result<AddSchedulePreferenceCommand> CreateFromRequest(
        AddSchedulePreferenceRequest request, Guid employeeId)
    {
        var employeeIdTyped = new EmployeeId(employeeId);
        StartEndTime? startEndTime;
        
        if (request.StartTime is null || request.EndTime is null)
        {
            startEndTime = null;
        }
        else
        {
            var timeResult = StartEndTime.Create(
                request.StartTime.Value, request.EndTime.Value);
            if (timeResult.IsFailure)
                return timeResult.MapToType<AddSchedulePreferenceCommand>();
            startEndTime = timeResult.Value;
        }
        
        return Result.Ok(new AddSchedulePreferenceCommand
        {
            EmployeeId = employeeIdTyped,
            Date = request.Date,
            StartEndTime = startEndTime,
            IsSetAsDayOff = request.SetAsDayOff
        });
    }
};