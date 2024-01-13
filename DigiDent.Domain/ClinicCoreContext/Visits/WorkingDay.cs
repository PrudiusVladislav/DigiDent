using DigiDent.Domain.ClinicCoreContext.Visits.Errors;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits;

public class WorkingDay: IEntity<WorkingDayId, Guid>
{
    public WorkingDayId Id { get; init; }
    public DateOnly Date { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    
    internal WorkingDay(
        WorkingDayId id,
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime)
    {
        Id = id;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
    }
    
    public static Result<WorkingDay> Create(
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime)
    {
        if (startTime >= endTime)
        {
            return Result.Fail<WorkingDay>(ScheduleErrors
                .WorkingDayStartTimeIsGreaterThanEndTime);
        }
        
        return Result.Ok(new WorkingDay(
            new WorkingDayId(Guid.NewGuid()), 
            date,
            startTime,
            endTime));
    }
}