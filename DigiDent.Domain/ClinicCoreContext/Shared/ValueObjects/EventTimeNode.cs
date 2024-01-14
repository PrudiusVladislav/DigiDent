using DigiDent.Domain.ClinicCoreContext.Shared.Extensions;

namespace DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;

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