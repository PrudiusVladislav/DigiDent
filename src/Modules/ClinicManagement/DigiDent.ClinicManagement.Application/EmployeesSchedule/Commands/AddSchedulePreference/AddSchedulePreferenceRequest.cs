
namespace DigiDent.ClinicManagement.Application.EmployeesSchedule.Commands.AddSchedulePreference;

public sealed record AddSchedulePreferenceRequest(
    DateOnly Date,
    TimeOnly? StartTime=null,
    TimeOnly? EndTime=null,
    bool SetAsDayOff=true);