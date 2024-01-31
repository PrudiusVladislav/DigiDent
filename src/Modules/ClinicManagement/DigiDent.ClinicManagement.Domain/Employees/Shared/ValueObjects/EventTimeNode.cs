
namespace DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;

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
    
    /// <summary>
    /// Checks if the event can fit between the previous and next nodes.
    /// </summary>
    /// <param name="previousNode">The previous node.</param>
    /// <param name="nextNode">The next node.</param>
    /// <returns></returns>
    internal bool EventCanFitBetween(
        EventTimeNode previousNode, EventTimeNode nextNode)
    {
        return !(StartTime < previousNode.EndTime ||
                 EndTime > nextNode.StartTime);
    }
}