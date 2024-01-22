using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;

namespace DigiDent.Application.ClinicCore.EmployeesSchedule.Commands.AddSchedulePreference;

public record AddSchedulePreferenceRequest(
    DateOnly Date,
    TimeOnly? StartTime=null,
    TimeOnly? EndTime=null,
    bool SetAsDayOff=true);