using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Shared.Extensions;

public static class EventTimeNodeExtensions
{
    internal static bool EventCanFitBetween(
        this EventTimeNode eventToFit,
        EventTimeNode previousNode,
        EventTimeNode nextNode)
    {
        return previousNode.StartTime.Add(eventToFit.Duration) <= nextNode.StartTime;
    }
    
}