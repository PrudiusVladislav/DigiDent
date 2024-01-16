using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;

public abstract class Employee: 
    AggregateRoot,
    IEmployee<EmployeeId, Guid>
{
    public EmployeeId Id { get; init; }
    public Email Email { get; protected set; }
    public PhoneNumber PhoneNumber { get; protected set;}
    public FullName FullName { get; protected set; }
    public Gender Gender { get; set; }
    public DateOnly? DateOfBirth { get; protected set;}
    public EmployeeStatus Status { get; protected set;}
    
    public ICollection<WorkingDay> WorkingDays { get; set; }
        = new List<WorkingDay>();
    public ICollection<SchedulePreference> SchedulePreferences { get; set; }
        = new List<SchedulePreference>();

    
    private const int LegalWorkingAge = 18;
    
    /// <summary>
    /// Validates the birth date of the employee.
    /// </summary>
    /// <param name="birthDate"> The birth date of the employee. </param>
    /// <typeparam name="TEmployee"> The type of the employee. </typeparam>
    /// <returns></returns>
    public static Result ValidateBirthDate<TEmployee>(DateOnly birthDate)
    {
        var age = DateTime.UtcNow.Year - birthDate.Year;
        
        if (age < LegalWorkingAge)
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
}