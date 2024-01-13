using DigiDent.Domain.ClinicCoreContext.Doctors.Errors;
using DigiDent.Domain.ClinicCoreContext.Doctors.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.Extensions;
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
    /// <param name="fromDateTime">The date time from which the available time slots are returned.</param>
    /// <param name="untilDate">The date until which the available date times are returned.</param>
    /// <param name="duration">The duration of the appointment.</param>
    /// <param name="timeStep">The time step between the available date times.</param>
    /// <returns></returns>
    public IReadOnlyCollection<DateTime> GetAvailableDateTimes(
        DateTime fromDateTime,
        DateOnly untilDate,
        TimeSpan duration,
        TimeSpan timeStep)
    {
        var availableDateTimes = new List<DateTime>();
        IOrderedEnumerable<WorkingDay> workingDaysToLookThrough = WorkingDays
            .GetRequestedWorkingDays(fromDateTime.ToDateOnly(), untilDate);

        foreach (var workingDay in workingDaysToLookThrough)
        {
            IReadOnlyList<EventTimeNode> workingDayEvents = workingDay
                .GetWorkingDayEventsNodes(Appointments, fromDateTime);

            for (var i = 0; i < workingDayEvents.Count - 1; i++)
            {
                var previousNode = workingDayEvents[i];
                var nextNode = workingDayEvents[i + 1];

                IReadOnlyList<DateTime> availableDateTimesBetweenNodes
                    = EventTimeNode.GetAllTimePointsBetweenNodes(
                        previousNode, nextNode, workingDay.Date,  duration, timeStep);

                availableDateTimes.AddRange(availableDateTimesBetweenNodes);
            }
        }

        return availableDateTimes;
    }
}