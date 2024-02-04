using DigiDent.ClinicManagement.Domain.Employees.Shared.Constants;
using DigiDent.ClinicManagement.Domain.Employees.Shared.Extensions;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Shared.Extensions;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Employees.Shared;

public class WorkingDay: IEntity<WorkingDayId, Guid>
{
    public WorkingDayId Id { get; init; }
    public DateOnly Date { get; private set; }
    public StartEndTime StartEndTime { get; private set; }
    
    public EmployeeId EmployeeId { get; init; }
    public Employee Employee { get; init; } = null!;
    
    internal WorkingDay(
        WorkingDayId id,
        DateOnly date,
        StartEndTime startEndTime,
        EmployeeId employeeId)
    {
        Id = id;
        Date = date;
        StartEndTime = startEndTime;
        EmployeeId = employeeId;
    }
    
    public static Result<WorkingDay> Create(
        DateOnly date,
        StartEndTime startEndTime,
        EmployeeId employeeId)
    {
        var workingDayId = TypedId.New<WorkingDayId>();
        return Result.Ok(new WorkingDay(
            workingDayId, 
            date,
            startEndTime,
            employeeId));
    }
    
    /// <summary>
    /// Checks if the working day starts before the given date time.
    /// </summary>
    /// <param name="time">The time to check.</param>
    /// <returns></returns>
    public bool StartsBefore(TimeOnly time)
    {
        return StartEndTime.StartTime < time;
    }
    
    /// <summary>
    /// Checks if the working day ends after the given date time.
    /// </summary>
    /// <param name="time">The time to check.</param>
    /// <returns></returns>
    public bool EndsAfter(TimeOnly time)
    {
        return StartEndTime.EndTime > time;
    }
    
    /// <summary>
    /// Returns the working day's events as a list of <see cref="EventTimeNode"/>.
    /// </summary>
    /// <param name="appointmentsOnWorkingDay">The appointments on the working day.</param>
    /// <param name="currentDate">The current date.</param>
    /// <param name="fromTime">The time from which the events are returned.</param>
    /// <returns></returns>
    internal IReadOnlyList<EventTimeNode> GetWorkingDayEventsNodes(
        IEnumerable<Appointment> appointmentsOnWorkingDay,
        DateOnly currentDate, //TODO: consider refactoring the DateOnly + TimeOnly to DateTime
        TimeOnly fromTime)
    {
        IEnumerable<EventTimeNode> workingDayAppointmentsEvents = appointmentsOnWorkingDay
            .ConvertToEventTimeNodes();
        
        List<EventTimeNode> workingDayEvents = [];
        
        if (Date == currentDate && StartsBefore(fromTime))
        {
            List<EventTimeNode> eventsAfterFromTime = workingDayAppointmentsEvents
                .GetNodesStartingFromTime(fromTime, isNextNode: false);
            
            workingDayEvents.AddRange(eventsAfterFromTime);
        } else
        {
            workingDayEvents.Add(new EventTimeNode(
                StartEndTime.StartTime, TimeSpan.Zero));
            workingDayEvents.AddRange(workingDayAppointmentsEvents);
        }
        
        EventTimeNode lastEventNode = new(StartEndTime.EndTime, TimeSpan.Zero);
        workingDayEvents.Add(lastEventNode);
        
        return workingDayEvents.AsReadOnly();
    }
    
    /// <summary>
    /// Returns available date times for the given working day.
    /// </summary>
    /// <param name="appointmentsOnWorkingDay">The appointments on the working day.</param>
    /// <param name="fromDateTime">The date time from which the available time slots are returned.</param>
    /// <param name="duration">The duration of the appointment.</param>
    /// <returns></returns>
    internal IReadOnlyList<DateTime> GetAvailableTimeSlots(
        IEnumerable<Appointment> appointmentsOnWorkingDay,
        DateTime fromDateTime,
        TimeDuration duration)
    {
        IReadOnlyList<EventTimeNode> workingDayEvents = GetWorkingDayEventsNodes(
            appointmentsOnWorkingDay,
            fromDateTime.ToDateOnly(),
            TimeOnly.FromDateTime(fromDateTime));

        List<DateTime> availableTimeSlots = [];
        for (var i = 0; i < workingDayEvents.Count - 1; i++)
        {
            EventTimeNode previousNode = workingDayEvents[i];
            EventTimeNode nextNode = workingDayEvents[i + 1];

            IReadOnlyList<DateTime> availableTimeSlotsBetweenEvents = (previousNode, nextNode)
                .GetAllTimeSlotsBetweenEvents(
                    Date,
                    duration,
                    ScheduleConstants.DefaultTimeStep);

            availableTimeSlots.AddRange(availableTimeSlotsBetweenEvents);
        }

        return availableTimeSlots.AsReadOnly();
    }
}