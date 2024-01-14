using DigiDent.Domain.ClinicCoreContext.Shared.Extensions;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared;

internal class EventTimeNode
{
    public TimeOnly StartTime { get; set; }
    public TimeSpan Duration { get; set; }

    public TimeOnly EndTime
    {
        get => StartTime.Add(Duration);
        set => Duration = value - StartTime;
    }

    internal EventTimeNode(
        TimeOnly startTime,
        TimeSpan duration)
    {
        StartTime = startTime;
        Duration = duration;
    }
    
}