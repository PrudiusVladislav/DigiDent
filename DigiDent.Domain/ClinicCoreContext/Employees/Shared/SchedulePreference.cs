using DigiDent.Domain.ClinicCoreContext.Employees.Assistants.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared;

public class SchedulePreference: IEntity<SchedulePreferenceId, Guid>
{
    public SchedulePreferenceId Id { get; init; }
    public DateOnly Date { get; private set; }
    public StartEndTime? StartEndTime { get; private set; }
    public bool IsSetAsDayOff { get; private set; }
    
    public IEmployeeId EmployeeId { get; private set; }
    public IEmployee Employee { get; private set; } = null!;
    
    internal SchedulePreference(
        SchedulePreferenceId id,
        DateOnly date,
        IEmployeeId employeeId,
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
        IEmployeeId employeeId,
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