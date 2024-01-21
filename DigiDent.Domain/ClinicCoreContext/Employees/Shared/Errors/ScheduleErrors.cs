using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.Errors;

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
}