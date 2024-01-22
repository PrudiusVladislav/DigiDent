using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.Extensions;

public static class EventTimeNodeExtensions
{
    /// <summary>
    /// Checks if the event can fit between the previous and next nodes.
    /// </summary>
    /// <param name="eventToFit">The event to fit.</param>
    /// <param name="previousNode">The previous node.</param>
    /// <param name="nextNode">The next node.</param>
    /// <returns></returns>
    internal static bool EventCanFitBetween(
        this EventTimeNode eventToFit,
        EventTimeNode previousNode,
        EventTimeNode nextNode)
    {
        return previousNode.StartTime.Add(eventToFit.Duration) <= nextNode.StartTime;
    }

    /// <summary>
    /// Returns the closest node to the given time.
    /// </summary>
    /// <param name="nodes">The collection of <see cref="EventTimeNode"/>s.</param>
    /// <param name="time">The time point.</param>
    /// <param name="isNextNode">If true, returns the next node, otherwise returns the previous node.</param>
    /// <returns></returns>
    internal static EventTimeNode? GetClosestNodeToTime(
        this IEnumerable<EventTimeNode> nodes,
        TimeOnly time,
        bool isNextNode=true)
    {
        var nodesList = nodes.ToList();
        if (nodesList.Count == 0) return null;
        
        var closestNode = nodesList.First();
        TimeSpan closestDifference = isNextNode
            ? (closestNode.StartTime - time)
            : (time - closestNode.EndTime);

        foreach (var node in nodesList.Skip(1))
        {
            TimeSpan currentDifference = isNextNode
                ? (node.StartTime - time)
                : (time - node.EndTime);

            if (Math.Abs(currentDifference.Ticks) < Math.Abs(closestDifference.Ticks))
            {
                closestNode = node;
                closestDifference = currentDifference;
            }
        }

        return closestNode;
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
        bool isNextNode = true)
    {
        var nodesList = nodes.OrderBy(node => node.StartTime).ToList();
        var resultNodes = new List<EventTimeNode>();

        if (nodesList.Count == 0) return resultNodes;

        int startIndex = isNextNode
            ? nodesList.FindIndex(node => node.StartTime > fromTime)
            : nodesList.FindLastIndex(node => node.StartTime < fromTime);

        if (startIndex == -1 || startIndex >= nodesList.Count) 
            return resultNodes;

        resultNodes = nodesList.GetRange(startIndex, nodesList.Count - startIndex);
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
    internal static IReadOnlyList<DateTime> GetAllTimePointsBetweenNodes(
        this (EventTimeNode, EventTimeNode) nodes,
        DateOnly date,
        TimeDuration duration,
        TimeSpan timeStep)
    {
        var availableTimePoints = new List<DateTime>();
        var eventNode = new EventTimeNode(
            startTime: nodes.Item1.EndTime,
            duration: duration.Duration);
        
        while (eventNode.EventCanFitBetween(nodes.Item1, nodes.Item2))
        {
            var dateTime = date.ToDateTime(eventNode.StartTime);
            availableTimePoints.Add(dateTime);
            eventNode.StartTime = eventNode.StartTime.Add(timeStep);
        }
        
        return availableTimePoints;
    }
    
}