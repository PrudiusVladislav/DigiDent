using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;

public record StartEndTime
{
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }
    
    internal StartEndTime(
        TimeOnly startTime,
        TimeOnly endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }
    
    public static Result<StartEndTime> Create(
        TimeOnly startTime,
        TimeOnly endTime)
    {
        if (startTime >= endTime)
        {
            return Result.Fail<StartEndTime>(ScheduleErrors
                .StartTimeIsGreaterThanEndTime);
        }
        
        return Result.Ok(new StartEndTime(
            startTime,
            endTime));
    }
}