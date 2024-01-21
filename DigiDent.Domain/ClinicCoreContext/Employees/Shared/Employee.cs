using System.Text;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared;

public abstract class Employee: 
    AggregateRoot,
    IEmployee<EmployeeId>
{
    public EmployeeId Id { get; init; }
    public Email Email { get; init; }
    public PhoneNumber PhoneNumber { get; protected set;}
    public FullName FullName { get; init; }
    public Gender Gender { get; set; }
    public DateOnly? DateOfBirth { get; protected set; }
    public EmployeeStatus Status { get; protected set;}
    
    public ICollection<WorkingDay> WorkingDays { get; protected set; }
        = new List<WorkingDay>();
    public ICollection<SchedulePreference> SchedulePreferences { get; protected set; }
        = new List<SchedulePreference>();

    public virtual Result Update(UpdateEmployeeDTO dto)
    {
        var validationResult = IsUpdateDtoValid(dto);
        if (validationResult.IsFailure) return validationResult;
        
        UpdateProperties(dto);
        return Result.Ok();
    }

    protected virtual void UpdateProperties(UpdateEmployeeDTO dto)
    {
        if (dto.Gender is not null && dto.Gender.Value != default)
            Gender = dto.Gender.Value;
        ChangeEmployeeStatus(dto.Status);
        DateOfBirth = dto.BirthDate ?? DateOfBirth;
    }
    
    protected virtual Result IsUpdateDtoValid(UpdateEmployeeDTO dto)
    {
        if (dto.BirthDate is not null)
            return IsLegalWorkingAge(dto.BirthDate.Value);
        
        return Result.Ok();
    }

    public abstract Result IsLegalWorkingAge(DateOnly birthDateToCheck);
    
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
            if (workingDay.Date >= startDate && 
                workingDay.Date <= endDate)
            {
                workTime += workingDay.StartEndTime.EndTime - 
                            workingDay.StartEndTime.StartTime;
            }
        }
        return workTime;
    }
    
    /// <summary>
    /// Encapsulates the logic for changing the status of the employee.
    /// </summary>
    /// <param name="status"> The new status of the employee. </param>
    protected virtual void ChangeEmployeeStatus(EmployeeStatus? status)
    {
        if (status is null || status.Value == default) return;
        
        if (status == EmployeeStatus.Dismissed)
        {
            WorkingDays.Clear();
            SchedulePreferences.Clear();
        }
        
        Status = status.Value;
        //TODO: Raise an event to email the employee and administrator about the status change.
    }
}