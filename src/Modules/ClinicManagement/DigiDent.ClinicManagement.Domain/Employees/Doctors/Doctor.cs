using DigiDent.ClinicManagement.Domain.Employees.Doctors.Errors;
using DigiDent.ClinicManagement.Domain.Employees.Doctors.ValueObjects;
using DigiDent.ClinicManagement.Domain.Employees.Shared;
using DigiDent.ClinicManagement.Domain.Employees.Shared.Extensions;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Shared.Extensions;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.ClinicManagement.Domain.Employees.Doctors;

public class Doctor : Employee
{
    public DoctorSpecialization Specialization { get; private set; }
    public string? Biography { get; set; }
    
    public ICollection<ProvidedService> ProvidedServices { get; set; } = new List<ProvidedService>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<PastVisit> PastVisits { get; set; } = new List<PastVisit>();

    internal Doctor(
        EmployeeId id,
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public static Doctor Create(PersonCreationArgs args)
    {
        var doctorId = TypedId.New<EmployeeId>();
        return new Doctor(
            doctorId,
            args.FullName,
            args.Email,
            args.PhoneNumber);
    }

    /// <summary>
    /// Returns available date time slots for the doctor.
    /// </summary>
    /// <param name="fromDateTime">The date time from which the available time slots are returned.</param>
    /// <param name="untilDate">The date until which the available date times are returned.</param>
    /// <param name="currentDateTime">The current date time.</param>
    /// <param name="duration">The duration of the appointment.</param>
    /// <returns></returns>
    public IReadOnlyCollection<DateTime> GetAvailableTimeSlots(
        DateTime fromDateTime,
        DateOnly untilDate,
        DateTime currentDateTime,
        TimeDuration duration)
    {
        List<DateTime> availableDateTimes = [];
        
        var workingDaysToLookThrough = WorkingDays
            .GetWorkingDaysBetweenDates(fromDateTime.ToDateOnly(), untilDate);

        foreach (var workingDay in workingDaysToLookThrough)
        {
            var availableTimeSlotsForDay = workingDay
                .GetAvailableTimeSlots(
                    Appointments.GetAppointmentsOnWorkingDay(workingDay),
                    currentDateTime,
                    duration);
            
            availableDateTimes.AddRange(availableTimeSlotsForDay);
        }

        return availableDateTimes;
    }

    /// <summary>
    /// Checks if the doctor is available at the given date time.
    /// </summary>
    /// <param name="dateTimeToCheck">The date time to check.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    /// <param name="duration">The duration of the considered appointment.</param>
    /// <returns></returns>
    public bool IsAvailableAt(
        DateTime dateTimeToCheck,
        IDateTimeProvider dateTimeProvider,
        TimeDuration duration)
    {
        var workingDay = WorkingDays.FirstOrDefault(wd => 
            wd.Date == dateTimeToCheck.ToDateOnly());
        if (workingDay is null) return false;
        
        DateTime currentDateTime = dateTimeProvider.Now;
        
        var workingDayEvents = workingDay
            .GetWorkingDayEventsNodes(
                Appointments,
                currentDateTime.ToDateOnly(), 
                currentDateTime.ToTimeOnly());
        
        var (previousEvent, nextEvent) = workingDayEvents
            .GetClosestNodesToTime(dateTimeToCheck.ToTimeOnly());
        
        if (previousEvent is null || nextEvent is null) return false;
        
        EventTimeNode appointmentNode = new(
            dateTimeToCheck.ToTimeOnly(), duration.Duration);
        
        return appointmentNode.EventCanFitBetween(previousEvent, nextEvent);
    }
    
    public Result Update(UpdateDoctorDTO dto)
    {
        var baseUpdateDTO = dto.ToUpdateEmployeeDTO;
        
        var validationResult = IsUpdateDtoValid(dto, baseUpdateDTO);
        if (validationResult.IsFailure) return validationResult;
        
        UpdateProperties(dto, baseUpdateDTO);
        
        return Result.Ok();
    }
    
    private void UpdateProperties(
        UpdateDoctorDTO dto, UpdateEmployeeDTO baseUpdateDTO)
    {
        base.UpdateProperties(baseUpdateDTO);
        
        if (dto.Specialization is not null && dto.Specialization.Value != default)
            Specialization = dto.Specialization.Value;
        
        Biography = dto.Biography ?? Biography;
    }
    
    private Result IsUpdateDtoValid(
        UpdateDoctorDTO dto, UpdateEmployeeDTO baseUpdateDTO)
    {
        var baseValidationResult = base.IsUpdateDtoValid(baseUpdateDTO);
        var biographyValidationResult = dto.Biography is null 
            ? Result.Ok()
            : ValidateBiography(dto.Biography);
        
        return Result.Merge(baseValidationResult, biographyValidationResult);
    }

    protected override Result IsLegalWorkingAge(DateOnly birthDateToCheck)
    { 
        const int legalWorkingAge = 18;
        return ValidateBirthDate<Doctor>(birthDateToCheck, legalWorkingAge);
    }

    private Result ValidateBiography(string biography)
    { 
        const int biographyMaxLength = 1000;
        
        if (biography.Length > biographyMaxLength)
            return Result.Fail(DoctorErrors.BiographyIsTooLong);

        Biography = biography;
        return Result.Ok();
    }
}