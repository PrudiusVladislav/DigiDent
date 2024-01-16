using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
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
}