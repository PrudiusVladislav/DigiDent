using System.Text.Json.Serialization;
using DigiDent.ClinicManagement.Domain.Employees.Shared.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;

public record StartEndTime
{
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }

    [JsonConstructor]
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