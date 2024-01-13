using DigiDent.Domain.ClinicCoreContext.Shared.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

/// <summary>
/// Contains helper methods for working with employees.
/// </summary>
public static class Employee
{
    private const int LegalWorkingAge = 16;
    
    public static Result ValidateBirthDate<T>(DateTime birthDate)
        where T : class, IEmployee
    {
        var age = DateTime.UtcNow.Year - birthDate.Year;
        
        if (age < LegalWorkingAge)
            return Result.Fail(EmployeeErrors
                .EmployeeIsTooYoung(nameof(T)));

        return Result.Ok();
    }
    
    public static TimeSpan EmployeeWorkTime(
        IEmployee employee,
        DateOnly startDate,
        DateOnly endDate)
    {
        var workTime = TimeSpan.Zero;
        
        foreach (var workingDay in employee.WorkingDays)
        {
            if (workingDay.Date >= startDate && workingDay.Date <= endDate)
            {
                workTime += workingDay.EndTime - workingDay.StartTime;
            }
        }
        return workTime;
    }
}