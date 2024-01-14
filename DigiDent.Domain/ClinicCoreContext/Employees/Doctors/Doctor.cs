using DigiDent.Domain.ClinicCoreContext.Employees.Doctors.Errors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Extensions;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.Extensions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Doctors;

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
    
    public Gender Gender { get; set; }
    public DateTime? DateOfBirth { get; private set; }

    public DoctorSpecialization? Specialization { get; private set; }
    public string? Biography { get; private set; }

    public ICollection<DentalProcedure> ProvidedServices { get; set; } = new List<DentalProcedure>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Visit> PastVisits { get; set; } = new List<Visit>();
    public ICollection<WorkingDay> WorkingDays { get; set; } = new List<WorkingDay>();
    public ICollection<SchedulePreference> SchedulePreferences { get; set; } 
        = new List<SchedulePreference>();

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
        var doctorId = TypedId.New<DoctorId>();
        return new Doctor(doctorId, fullName, email, phoneNumber);
    }

    public void SetSpecialization(DoctorSpecialization specialization)
    {
        Specialization = specialization;
    }

    private const int BiographyMaxLength = 1000;

    public Result SetBiography(string biography)
    {
        if (biography.Length > BiographyMaxLength)
            return Result.Fail(DoctorErrors.BiographyIsTooLong);

        Biography = biography;
        return Result.Ok();
    }

    /// <summary>
    /// Returns available date times for the doctor.
    /// </summary>
    /// <param name="fromDateTime">The date time from which the available time slots are returned.</param>
    /// <param name="untilDate">The date until which the available date times are returned.</param>
    /// <param name="duration">The duration of the appointment.</param>
    /// <returns></returns>
    public IReadOnlyCollection<DateTime> GetAvailableDateTimes(
        DateTime fromDateTime,
        DateOnly untilDate,
        TimeSpan duration)
    {
        var availableDateTimes = new List<DateTime>();
        IOrderedEnumerable<WorkingDay> workingDaysToLookThrough = WorkingDays
            .GetRequestedWorkingDays(fromDateTime.ToDateOnly(), untilDate);

        foreach (var workingDay in workingDaysToLookThrough)
        {
            var availableDateTimesForDay = workingDay
                .GetAvailableDateTimesForDay(
                    Appointments, fromDateTime, duration);
            
            availableDateTimes.AddRange(availableDateTimesForDay);
        }

        return availableDateTimes;
    }

    /// <summary>
    /// Checks if the doctor is available at the given date time.
    /// </summary>
    /// <param name="dateTimeToCheck">The date time to check.</param>
    /// <param name="currentDateTime">The current date time.</param>
    /// <param name="duration">The duration of the considered appointment.</param>
    /// <returns></returns>
    public bool IsAvailableAt(
        DateTime dateTimeToCheck,
        DateTime currentDateTime,
        TimeSpan duration)
    {
        var workingDay = WorkingDays.FirstOrDefault(wd => 
            wd.Date == dateTimeToCheck.ToDateOnly());
        if (workingDay is null) return false;
        
        var availableDateTimes = workingDay
            .GetAvailableDateTimesForDay(
                Appointments, currentDateTime, duration);
        
        return availableDateTimes.Any(slot => 
            slot.Hour == dateTimeToCheck.Hour && 
            slot.Minute == dateTimeToCheck.Minute);
    }
}