using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Errors;

public static class ScheduleErrors
{
    public static Error WorkingDayStartTimeIsGreaterThanEndTime 
        => new(
            ErrorType.Validation,
            nameof(WorkingDay),
            "Working day start time is greater than end time");
    
}