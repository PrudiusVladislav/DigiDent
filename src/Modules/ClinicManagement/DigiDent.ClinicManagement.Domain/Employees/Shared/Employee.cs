using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Shared.Domain.Abstractions;
using DigiDent.Shared.Domain.ReturnTypes;
using DigiDent.Shared.Domain.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared;

public abstract class Employee: 
    AggregateRoot,
    IEmployee<EmployeeId>
{
    public EmployeeId Id { get; init; } = null!;
    public Email Email { get; init; } = null!;
    public PhoneNumber PhoneNumber { get; protected set;} = null!;
    public FullName FullName { get; init; } = null!;
    public Gender Gender { get; set; }
    public DateOnly? DateOfBirth { get; protected set; }
    public EmployeeStatus Status { get; protected set;}
    
    public ICollection<WorkingDay> WorkingDays { get; protected set; }
        = new List<WorkingDay>();
    public ICollection<SchedulePreference> SchedulePreferences { get; protected set; }
        = new List<SchedulePreference>();
    
    /// <summary>
    /// Adds a working day to the employee's working days.
    /// </summary>
    /// <param name="workingDay"> The working day to be added. </param>
    /// <returns> A result indicating whether the addition was successful or not. </returns>
    public Result AddWorkingDay(WorkingDay workingDay)
    {
        var validationResult = IsWorkingDayValid(workingDay);
        if (validationResult.IsFailure) return validationResult;
        
        WorkingDays.Add(workingDay);
        return Result.Ok();
    }
    
    private Result IsWorkingDayValid(WorkingDay workingDay)
    {
        var validationResult = new Result();
        
        var isDateEmpty = WorkingDays.All(wd => wd.Date != workingDay.Date);
        if (isDateEmpty is false)
            validationResult.AddError(ScheduleErrors
                .WorkingDayIsAlreadySet(Id, workingDay.Date));
        
        var conflictsWithSchedulePreferences = SchedulePreferences.Any(sp => 
            sp.Date == workingDay.Date && 
            (sp.IsSetAsDayOff || 
             workingDay.StartsBefore(sp.StartEndTime!.StartTime) || 
             workingDay.EndsAfter(sp.StartEndTime.EndTime)));
        
        if (conflictsWithSchedulePreferences)
            validationResult.AddError(ScheduleErrors
                .WorkingDayConflictsWithSchedulePreference(Id, workingDay.Date));
        
        return validationResult;
    }
    
    /// <summary>
    /// Adds a schedule preference to the employee's schedule preferences.
    /// </summary>
    /// <param name="schedulePreference"> The schedule preference to be added. </param>
    /// <returns> A result indicating whether the addition was successful or not. </returns>
    public Result AddSchedulePreference(SchedulePreference schedulePreference)
    {
        var validationResult = IsSchedulePreferenceValid(schedulePreference);
        if (validationResult.IsFailure) return validationResult;
        
        SchedulePreferences.Add(schedulePreference);
        return Result.Ok();
    }

    private Result IsSchedulePreferenceValid(SchedulePreference schedulePreference)
    {
        const int allowedSchedulePreferences = 2;
        var validationResult = new Result();
        
        var isPreferencesCountAtLimit = SchedulePreferences.Count == allowedSchedulePreferences;
        if (isPreferencesCountAtLimit)
            validationResult.AddError(ScheduleErrors
                .SchedulePreferenceCountLimitReached(Id, allowedSchedulePreferences));
        
        var isDateEmpty = SchedulePreferences.All(sp => sp.Date != schedulePreference.Date);
        if (isDateEmpty is false)
            validationResult.AddError(ScheduleErrors
                .SchedulePreferenceIsAlreadySet(Id, schedulePreference.Date));

        var conflictsWithWorkingDay = WorkingDays.Any(wd =>
            wd.Date == schedulePreference.Date);
        if (conflictsWithWorkingDay)
            validationResult.AddError(ScheduleErrors
                .SchedulePreferenceConflictsWithWorkingDay(Id, schedulePreference.Date));
        

        return validationResult;
    }
    
    /// <summary>
    /// Calculates the work time of the employee for the given period.
    /// </summary>
    /// <param name="startDate"> The start date of the period. </param>
    /// <param name="endDate"> The end date of the period. </param>
    /// <returns></returns>
    public TimeSpan GetWorkTime(DateOnly startDate, DateOnly endDate)
    {
        var workTime = TimeSpan.Zero;
        
        foreach (var workingDay in WorkingDays)
        {
            if (workingDay.Date >= startDate && workingDay.Date <= endDate)
            {
                workTime += workingDay.StartEndTime.EndTime - 
                            workingDay.StartEndTime.StartTime;
            }
        }
        return workTime;
    }
    
    /// <summary>
    /// Validates new values and updates the properties of the employee if the validation is successful. 
    /// </summary>
    /// <param name="dto"> The DTO containing the new values. </param>
    /// <returns> A result indicating whether the update was successful or not. </returns>
    public virtual Result Update(UpdateEmployeeDTO dto)
    {
        var validationResult = IsUpdateDtoValid(dto);
        if (validationResult.IsFailure) return validationResult;
        
        UpdateProperties(dto);
        return Result.Ok();
    }
    
    /// <summary>
    /// Encapsulates the logic for updating the properties of the employee. No validation is performed.
    /// </summary>
    protected virtual void UpdateProperties(UpdateEmployeeDTO dto)
    {
        if (dto.Gender is not null && dto.Gender.Value != default)
            Gender = dto.Gender.Value;
        ChangeEmployeeStatus(dto.Status);
        DateOfBirth = dto.BirthDate ?? DateOfBirth;
    }
    
    /// <summary>
    /// Encapsulates the rules for validating the update DTO values.
    /// </summary>
    /// <returns> A result indicating whether the update DTO is valid or not. </returns>
    protected virtual Result IsUpdateDtoValid(UpdateEmployeeDTO dto)
    {
        if (dto.BirthDate is not null)
            return IsLegalWorkingAge(dto.BirthDate.Value);
        
        return Result.Ok();
    }
    
    /// <summary>
    /// Validates the birth date of the employee. Must be implemented by the derived classes.
    /// </summary>
    /// <param name="birthDateToCheck"> The birth date of the employee. </param>
    /// <returns> A result indicating whether the birth date is valid or not. </returns>
    protected abstract Result IsLegalWorkingAge(DateOnly birthDateToCheck);
    
    /// <summary>
    /// Validates the birth date of the employee.
    /// </summary>
    /// <param name="birthDate"> The birth date of the employee. </param>
    /// <typeparam name="TEmployee"> The type of the employee. </typeparam>
    /// <param name="minAllowedWorkingAge"> The minimum allowed working age of the employee. </param>
    /// <returns></returns>
    protected Result ValidateBirthDate<TEmployee>(
        DateOnly birthDate, int minAllowedWorkingAge)
    {
        var age = DateTime.UtcNow.Year - birthDate.Year;
        
        if (age < minAllowedWorkingAge)
            return Result.Fail(EmployeeErrors
                .EmployeeIsTooYoung(nameof(TEmployee)));

        return Result.Ok();
    }
    
    /// <summary>
    /// Encapsulates the logic for changing the status of the employee.
    /// </summary>
    /// <param name="status"> The new status of the employee. </param>
    protected virtual void ChangeEmployeeStatus(EmployeeStatus? status)
    {
        if (status is null || status.Value == default) 
            return;
        
        if (status == EmployeeStatus.Dismissed)
        {
            WorkingDays.Clear();
            SchedulePreferences.Clear();
        }
        
        Status = status.Value;
        //TODO: Raise an event to email the employee and administrator about the status change.
    }
}