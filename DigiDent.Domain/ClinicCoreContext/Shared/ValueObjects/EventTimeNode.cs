namespace DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;

internal record EventTimeNode(TimeOnly StartTime, TimeOnly EndTime)
{ 
    internal static bool EventCanFitBetween(
        TimeSpan eventDuration,
        EventTimeNode previousNode,
        EventTimeNode nextNode)
    {
        return (nextNode.StartTime - previousNode.EndTime) >= eventDuration;
    }
}