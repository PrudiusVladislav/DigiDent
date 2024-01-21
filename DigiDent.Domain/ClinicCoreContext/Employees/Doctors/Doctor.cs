using DigiDent.Domain.ClinicCoreContext.Employees.Doctors.Errors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Extensions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Shared.Extensions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Doctors;

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
    /// <param name="duration">The duration of the appointment.</param>
    /// <returns></returns>
    public IReadOnlyCollection<DateTime> GetAvailableTimeSlots(
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

    public override Result IsLegalWorkingAge(DateOnly birthDateToCheck)
    {
        const int legalWorkingAge = 18;
        return ValidateBirthDate<Doctor>(birthDateToCheck, legalWorkingAge);
    }
    
    public Result ValidateBiography(string biography)
    {
        const int biographyMaxLength = 1000;
        
        if (biography.Length > biographyMaxLength)
            return Result.Fail(DoctorErrors.BiographyIsTooLong);

        Biography = biography;
        return Result.Ok();
    }
}