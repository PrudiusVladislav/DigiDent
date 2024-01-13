using DigiDent.Domain.ClinicCoreContext.Shared.Extensions;

namespace DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;

internal class EventTimeNode
{
    public TimeOnly StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    
    public TimeOnly EndTime => StartTime.Add(Duration);

    internal EventTimeNode(
        TimeOnly startTime,
        TimeSpan duration)
    {
        StartTime = startTime;
        Duration = duration;
    }
    
    /// <summary>
    /// Return all possible time points between the given nodes.
    /// </summary>
    /// <param name="previousNode">The previous node of an event.</param>
    /// <param name="nextNode">The next node of an event.</param>
    /// <param name="date">The date.</param>
    /// <param name="duration">The duration of the event to fit between the two nodes.</param>
    /// <param name="timeStep">The time step to use when generating the time points.</param>
    /// <returns></returns>
    internal static IReadOnlyList<DateTime> GetAllTimePointsBetweenNodes(
        EventTimeNode previousNode,
        EventTimeNode nextNode,
        DateOnly date,
        TimeSpan duration,
        TimeSpan timeStep)
    {
        var availableTimePoints = new List<DateTime>();
        var eventNode = new EventTimeNode(
            startTime: previousNode.EndTime,
            duration: duration);
        
        while (eventNode.EventCanFitBetween(previousNode, nextNode))
        {
            var dateTime = date.ToDateTime(eventNode.StartTime);
            availableTimePoints.Add(dateTime);
            eventNode.StartTime = eventNode.StartTime.Add(timeStep);
        }
        
        return availableTimePoints;
    }
}