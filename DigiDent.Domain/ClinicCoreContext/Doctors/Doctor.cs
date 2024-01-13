using DigiDent.Domain.ClinicCoreContext.Doctors.Errors;
using DigiDent.Domain.ClinicCoreContext.Doctors.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Doctors;

public class Doctor :
    AggregateRoot,
    IEntity<DoctorId, Guid>,
    IPerson,
    IEmployee
{
    public DoctorId Id { get; init; }
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }
    public Gender Gender { get; }
    public DateTime? DateOfBirth { get; private set; }

    public DoctorSpecialization? Specialization { get; private set; }
    public string? Biography { get; private set; }

    public ICollection<DentalProcedure> ProvidedServices { get; set; } = new List<DentalProcedure>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Visit> PastVisits { get; set; } = new List<Visit>();
    public ICollection<WorkingDay> WorkingDays { get; set; } = new List<WorkingDay>();

    internal Doctor(
        DoctorId id,
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public static Doctor Create(
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber)
    {
        return new Doctor(
            new DoctorId(Guid.NewGuid()),
            fullName,
            email,
            phoneNumber);
    }

    public void SetSpecialization(DoctorSpecialization specialization)
    {
        Specialization = specialization;
    }

    private const int BiographyMaxLength = 1000;

    public Result SetBiography(string biography)
    {
        if (biography.Length > BiographyMaxLength)
        {
            return Result.Fail(DoctorErrors.BiographyIsTooLong);
        }

        Biography = biography;
        return Result.Ok();
    }

    /// <summary>
    /// Returns available date times for the doctor.
    /// </summary>
    /// <param name="untilDate">The date until which the available date times are returned.</param>
    /// <param name="duration">The duration of the appointment.</param>
    /// <returns></returns>
    public IReadOnlyCollection<DateTime> GetAvailableDateTimes(
        DateOnly untilDate,
        TimeSpan duration)
    {
        var availableDateTimes = new List<DateTime>();
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        
        IOrderedEnumerable<WorkingDay> workingDaysToLookThrough 
            = GetRequestedWorkingDays(untilDate, currentDate);

        //TODO: check for time within the day:
        //the case when there is a time within the current day,
        //but it (the time) has already passed
        foreach (var workingDay in workingDaysToLookThrough)
        {
            IReadOnlyList<EventTimeNode> workingDayEvents 
                = GetWorkingDayEventsNodes(workingDay);
            
            for (var i = 0; i < workingDayEvents.Count - 1; i++)
            {
                var previousNode = workingDayEvents[i];
                var nextNode = workingDayEvents[i + 1];
                if (EventTimeNode.EventCanFitBetween(duration, previousNode, nextNode))
                {
                    availableDateTimes.Add(
                        ParseDateTime(workingDay.Date, previousNode.EndTime));
                }
            }
        }

        return availableDateTimes;
    }
    
    //TODO: move some abstract methods out of the class

    private IReadOnlyList<EventTimeNode> GetWorkingDayEventsNodes(WorkingDay workingDay)
    {
        //TODO: possibly convert to extension methods
        IEnumerable<EventTimeNode> appointmentsTimeNodes 
            = ConvertToTimeNodes(
                GetAppointmentsOnWorkingDay(workingDay));

        List<EventTimeNode> workingDayEvents = [];
        var workStartNode = new EventTimeNode(
            workingDay.StartTime, workingDay.StartTime);
        var workEndNode = new EventTimeNode(
            workingDay.EndTime, workingDay.EndTime);
            
        workingDayEvents.Add(workStartNode);
        workingDayEvents.AddRange(appointmentsTimeNodes);
        workingDayEvents.Add(workEndNode);
        return workingDayEvents.AsReadOnly();
    }

    private IOrderedEnumerable<WorkingDay> GetRequestedWorkingDays(
        DateOnly untilDate,
        DateOnly currentDate)
    {
        return WorkingDays
            .Where(wd => wd.Date >= currentDate && wd.Date <= untilDate)
            .OrderBy(wd => wd.Date);
    }

    private IOrderedEnumerable<Appointment> GetAppointmentsOnWorkingDay(
        WorkingDay workingDay)
    {
        return Appointments
            .Where(a => DateOnly.FromDateTime(
                a.VisitDateTime.Date)== workingDay.Date)
            .OrderBy(a => a.VisitDateTime);
    }

    private IEnumerable<EventTimeNode> ConvertToTimeNodes(
        IEnumerable<Appointment> appointments)
    {
        return appointments
            .Select(a => new EventTimeNode(
                TimeOnly.FromDateTime(a.VisitDateTime),
                TimeOnly.FromDateTime(a.VisitDateTime + a.Duration)));
    }

    public static DateTime ParseDateTime(DateOnly date, TimeOnly time)
    {
        return DateTime.Parse($"{date} {time}");
    }
}