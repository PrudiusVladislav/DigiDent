using DigiDent.Domain.ClinicCoreContext.Shared.Constants;
using DigiDent.Domain.ClinicCoreContext.Shared.Extensions;
using DigiDent.Domain.ClinicCoreContext.Visits;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.Extensions;

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
    /// <param name="time">The time to check.</param>
    /// <returns></returns>
    internal static bool StartsBefore(this WorkingDay workingDay, TimeOnly time)
    {
        return workingDay.StartTime < time;
    }

    /// <summary>
    /// Returns the working day's events as a list of <see cref="EventTimeNode"/>.
    /// </summary>
    /// <param name="workingDay">The specified working day.</param>
    /// <param name="appointments">The appointments collection.</param>
    /// <param name="currentDate">The current date.</param>
    /// <param name="fromTime">The time from which the events are returned.</param>
    /// <returns></returns>
    internal static IReadOnlyList<EventTimeNode> GetWorkingDayEventsNodes(
        this WorkingDay workingDay,
        IEnumerable<Appointment> appointments,
        DateOnly currentDate, //TODO: consider refactoring the DateOnly + TimeOnly to DateTime
        TimeOnly fromTime)
    {
        List<EventTimeNode> allWorkingDayAppointments = appointments
            .GetAppointmentsOnWorkingDay(workingDay)
            .ConvertToEventTimeNodes()
            .ToList();
        
        List<EventTimeNode> workingDayEvents = [];
        
        if (workingDay.Date == currentDate && workingDay.StartsBefore(fromTime))
        {
            List<EventTimeNode> eventsAfterFromTime = allWorkingDayAppointments
                .GetNodesStartingFromTime(fromTime, isNextNode: false);
            
            workingDayEvents.AddRange(eventsAfterFromTime);
        } else
        {
            workingDayEvents.Add(new EventTimeNode(workingDay.StartTime, TimeSpan.Zero));
            workingDayEvents.AddRange(allWorkingDayAppointments);
        }
        
        var lastEventNode = new EventTimeNode(
            workingDay.EndTime, TimeSpan.Zero);
        workingDayEvents.Add(lastEventNode);
        
        return workingDayEvents.AsReadOnly();
    }
    
    /// <summary>
    /// Returns available date times for the given working day.
    /// </summary>
    /// <param name="workingDay"> The working day.</param>
    /// <param name="appointments">The appointments collection.</param>
    /// <param name="fromDateTime">The date time from which the available time slots are returned.</param>
    /// <param name="duration">The duration of the appointment.</param>
    /// <returns></returns>
    internal static  IReadOnlyList<DateTime> GetAvailableDateTimesForDay(
        this WorkingDay workingDay,
        IEnumerable<Appointment> appointments,
        DateTime fromDateTime,
        TimeSpan duration)
    {
        IReadOnlyList<EventTimeNode> workingDayEvents = workingDay
            .GetWorkingDayEventsNodes(
                appointments,
                fromDateTime.Date.ToDateOnly(),
                TimeOnly.FromDateTime(fromDateTime));

        var availableDateTimes = new List<DateTime>();
        for (var i = 0; i < workingDayEvents.Count - 1; i++)
        {
            var previousNode = workingDayEvents[i];
            var nextNode = workingDayEvents[i + 1];

            IReadOnlyList<DateTime> availableDateTimesBetweenNodes = (previousNode, nextNode)
                .GetAllTimePointsBetweenNodes(
                    workingDay.Date,
                    duration,
                    ScheduleConstants.DefaultTimeStep);

            availableDateTimes.AddRange(availableDateTimesBetweenNodes);
        }

        return availableDateTimes.AsReadOnly();
    }
}