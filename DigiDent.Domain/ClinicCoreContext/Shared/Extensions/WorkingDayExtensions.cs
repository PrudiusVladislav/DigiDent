using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;

namespace DigiDent.Domain.ClinicCoreContext.Shared.Extensions;

public static class WorkingDayExtensions
{
    /// <summary>
    /// Gets working days from the given collection that are in the requested date range.
    /// </summary>
    /// <param name="workingDays">The working days collection.</param>
    /// <param name="fromDate">The start date.</param>
    /// /// <param name="untilDate">The end date.</param>
    /// <returns></returns>
    internal static IOrderedEnumerable<WorkingDay> GetRequestedWorkingDays(
        this IEnumerable<WorkingDay> workingDays,
        DateOnly fromDate,
        DateOnly untilDate)
    {
        return workingDays
            .Where(wd => wd.Date >= fromDate && wd.Date <= untilDate)
            .OrderBy(wd => wd.Date);
    } 
    
    /// <summary>
    /// Checks if the working day starts before the given date time.
    /// </summary>
    /// <param name="workingDay">The working day.</param>
    /// <param name="dateTime">The date time to check.</param>
    /// <returns></returns>
    internal static bool StartsBefore(this WorkingDay workingDay, DateTime dateTime)
    {
        var workingDayStartDateTime = workingDay.Date
            .ToDateTime(workingDay.StartTime);
        return workingDayStartDateTime < dateTime;
    }

    internal static IReadOnlyList<EventTimeNode> GetWorkingDayEventsNodes(
        this WorkingDay workingDay,
        IEnumerable<Appointment> appointments,
        DateTime fromDateTime)
    {
        IEnumerable<EventTimeNode> appointmentsTimeNodes = appointments
            .GetAppointmentsOnWorkingDay(workingDay)
            .ConvertToEventTimeNodes();
        
        List<EventTimeNode> workingDayEvents = [];

        EventTimeNode firstEventNode;
        if (workingDay.StartsBefore(fromDateTime))
        {
            var fromTime = TimeOnly.FromDateTime(fromDateTime);
            firstEventNode = new EventTimeNode(fromTime, TimeSpan.Zero);
        } else
        {
            firstEventNode = new EventTimeNode(workingDay.StartTime, TimeSpan.Zero);
        }
        
        var lastEventNode = new EventTimeNode(
            workingDay.EndTime, TimeSpan.Zero);
        
        workingDayEvents.Add(firstEventNode);
        workingDayEvents.AddRange(appointmentsTimeNodes);
        workingDayEvents.Add(lastEventNode);
        
        return workingDayEvents.AsReadOnly();
    }
}