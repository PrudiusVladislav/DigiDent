using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Employees.Shared.Errors;

public static class ScheduleErrors
{
    public static Error StartTimeIsGreaterThanEndTime
        => new(
            ErrorType.Validation,
            nameof(StartEndTime),
            "Start time can not be greater than end time");
    
    public static Error StartEndTimeShouldBeSet
        => new(
            ErrorType.Validation,
            nameof(SchedulePreference),
            "Start time and end time should be set if day is not set as day off");
    
    public static Error WorkingDayIsAlreadySet(
        EmployeeId employeeId, DateOnly date) => new(
            ErrorType.Conflict,
            nameof(WorkingDay),
            $"Employee with id {employeeId.Value} already has a working day on {date}");
    
    public static Error WorkingDayConflictsWithSchedulePreference(
        EmployeeId employeeId, DateOnly date) => new(
            ErrorType.Conflict,
            nameof(WorkingDay),
            $"The working day on {date} conflicts with a schedule preference" + 
            $" of employee with id '{employeeId.Value}'");
    
    public static Error SchedulePreferenceIsAlreadySet(
        EmployeeId employeeId, DateOnly date) => new(
        ErrorType.Conflict,
        nameof(SchedulePreference),
        $"Employee with id {employeeId.Value} already set a schedule preference on {date}");
    
    public static Error SchedulePreferenceConflictsWithWorkingDay(
        EmployeeId employeeId, DateOnly date) => new(
        ErrorType.Conflict,
        nameof(SchedulePreference),
        $"It is not allowed to set a schedule preference on {date} because" +
        $" the employee with id '{employeeId.Value}' already has a working day on that date");
    
    public static Error SchedulePreferenceCountLimitReached(
        EmployeeId employeeId, int limit) => new(
        ErrorType.Validation,
    nameof(SchedulePreference),
    $"Employee with id {employeeId.Value} has reached the limit of {limit} schedule preferences");
}