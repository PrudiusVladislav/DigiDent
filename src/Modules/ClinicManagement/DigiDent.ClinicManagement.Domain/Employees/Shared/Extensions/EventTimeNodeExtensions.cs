using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;

namespace DigiDent.ClinicManagement.Domain.Employees.Shared.Extensions;

public static class EventTimeNodeExtensions
{
    /// <summary>
    /// Returns the closest node to the given time.
    /// </summary>
    /// <param name="nodes">The collection of <see cref="EventTimeNode"/>s.</param>
    /// <param name="time">The time point.</param>
    /// <returns></returns>
    internal static (EventTimeNode?, EventTimeNode?) GetClosestNodesToTime(
        this IEnumerable<EventTimeNode> nodes,
        TimeOnly time)
    {
        List<EventTimeNode> sourceNodes = nodes
            .OrderBy(node => node.StartTime)
            .ToList();
        
        if (sourceNodes.Count == 0) 
            return (null, null);

        int indexOfFirstNextEvent = sourceNodes.FindIndex(
            node => node.StartTime > time);
        
        if (indexOfFirstNextEvent == -1) 
            return (sourceNodes.Last(), null);
        if (indexOfFirstNextEvent == 0) 
            return (null, sourceNodes.First());
        
        int indexOfPreviousEvent = indexOfFirstNextEvent - 1;
        return (
            sourceNodes[indexOfPreviousEvent],
            sourceNodes[indexOfFirstNextEvent]);
    }
    
    /// <summary>
    /// Returns the nodes starting from the given time.
    /// </summary>
    /// <param name="nodes">The collection of <see cref="EventTimeNode"/>s.</param>
    /// <param name="fromTime">The time from which the nodes are returned.</param>
    /// <param name="isNextNode">If true, returns the next node, otherwise returns the previous node.</param>
    /// <returns></returns>
    internal static List<EventTimeNode> GetNodesStartingFromTime(
        this IEnumerable<EventTimeNode> nodes,
        TimeOnly fromTime,
        bool isNextNode = false)
    {
        List<EventTimeNode> sourceNodes = nodes.OrderBy(node => node.StartTime).ToList();
        List<EventTimeNode> resultNodes = [];

        if (sourceNodes.Count == 0) return resultNodes;

        int startIndex = isNextNode
            ? sourceNodes.FindIndex(node => node.StartTime > fromTime)
            : sourceNodes.FindLastIndex(node => node.StartTime < fromTime);

        if (startIndex == -1 || startIndex >= sourceNodes.Count) 
            return resultNodes;

        resultNodes = sourceNodes.GetRange(startIndex, sourceNodes.Count - startIndex);
        if (isNextNode is false)
        {
            // If the first node, that starts before the given time,
            // ends also before the given time,
            // then the first node is replaced with event node of the given time.
            if (resultNodes.First().EndTime < fromTime)
                resultNodes[0] = new EventTimeNode(fromTime, TimeSpan.Zero);
        }
        
        return resultNodes;
    }
    
    /// <summary>
    /// Return all possible time points between the given nodes.
    /// </summary>
    /// <param name="nodes">The tuple of <see cref="EventTimeNode"/>s.</param>
    /// <param name="date">The date.</param>
    /// <param name="duration">The duration of the event to fit between the two nodes.</param>
    /// <param name="timeStep">The time step to use when generating the time points.</param>
    /// <returns></returns>
    internal static IReadOnlyList<DateTime> GetAllTimeSlotsBetweenEvents(
        this (EventTimeNode, EventTimeNode) nodes,
        DateOnly date,
        TimeDuration duration,
        TimeSpan timeStep)
    {
        List<DateTime> availableTimeSlots= [];
        EventTimeNode previousEvent = nodes.Item1;
        EventTimeNode nextEvent = nodes.Item2;
        
        EventTimeNode eventNode = new(
            startTime: previousEvent.EndTime,
            duration.Duration);
        
        while (eventNode.EventCanFitBetween(previousEvent, nextEvent))
        {
            DateTime timeSlot = date.ToDateTime(eventNode.StartTime);
            availableTimeSlots.Add(timeSlot);
            eventNode.StartTime = eventNode.StartTime.Add(timeStep);
        }
        
        return availableTimeSlots;
    }
    
}