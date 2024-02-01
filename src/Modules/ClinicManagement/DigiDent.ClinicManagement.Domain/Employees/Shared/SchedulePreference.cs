using DigiDent.ClinicManagement.Domain.Employees.Shared.Errors;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Employees.Shared;

public class SchedulePreference: IEntity<SchedulePreferenceId, Guid>
{
    public SchedulePreferenceId Id { get; init; }
    public DateOnly Date { get; private set; }
    public StartEndTime? StartEndTime { get; private set; }
    public bool IsSetAsDayOff { get; private set; }
    public EmployeeId EmployeeId { get; init; }
    public Employee Employee { get; init; } = null!;
    
    internal SchedulePreference(
        SchedulePreferenceId id,
        DateOnly date,
        EmployeeId employeeId,
        StartEndTime? startEndTime=null,
        bool isSetAsDayOff=true)
    {
        Id = id;
        Date = date;
        EmployeeId = employeeId;
        StartEndTime = startEndTime;
        IsSetAsDayOff = isSetAsDayOff;
    }
    
    public static Result<SchedulePreference> Create(
        DateOnly date,
        EmployeeId employeeId,
        StartEndTime? startEndTime=null,
        bool isSetAsDayOff=true)
    {
        if (isSetAsDayOff is false && startEndTime is null)
        {
            return Result.Fail<SchedulePreference>(ScheduleErrors
                .StartEndTimeShouldBeSet);
        }
        
        var schedulePreferenceId = TypedId.New<SchedulePreferenceId>();
        return Result.Ok(new SchedulePreference(
            schedulePreferenceId, 
            date,
            employeeId,
            startEndTime,
            isSetAsDayOff));
    }
}