using DigiDent.Domain.ClinicCoreContext.Shared.Errors;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;

/// <summary>
/// Contains helper methods for working with employees.
/// </summary>
public static class EmployeeHelper
{
    private const int LegalWorkingAge = 16;
    
    /// <summary>
    /// Validates the birth date of the employee.
    /// </summary>
    /// <param name="birthDate"> The birth date of the employee. </param>
    /// <typeparam name="T"> The type of the employee. </typeparam>
    /// <typeparam name="TId"> The type of the employee id. </typeparam>
    /// <typeparam name="TIdValue"> The primitive type of the employee id value. </typeparam>
    /// <returns></returns>
    public static Result ValidateBirthDate<T, TId, TIdValue>(DateOnly birthDate)
        where T : class, IEmployee<TId, TIdValue>
        where TId : IEmployeeId<TIdValue>
        where TIdValue : notnull
    {
        var age = DateTime.UtcNow.Year - birthDate.Year;
        
        if (age < LegalWorkingAge)
            return Result.Fail(EmployeeErrors
                .EmployeeIsTooYoung(nameof(T)));

        return Result.Ok();
    }
    
    /// <summary>
    /// Calculates the work time of the employee for the given period.
    /// </summary>
    /// <param name="employee"> The employee. </param>
    /// <param name="startDate"> The start date of the period. </param>
    /// <param name="endDate"> The end date of the period. </param>
    /// <returns></returns>
    public static TimeSpan EmployeeWorkTime<TId, TIdValue>(
        IEmployee<TId, TIdValue> employee,
        DateOnly startDate,
        DateOnly endDate)
        where TId : IEmployeeId<TIdValue>
        where TIdValue : notnull
    {
        var workTime = TimeSpan.Zero;
        
        foreach (var workingDay in employee.WorkingDays)
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