using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;

namespace DigiDent.Application.ClinicCore.EmployeesSchedule.Queries.GetSchedulePreferencesForEmployee;

public class SchedulePreferenceDTO
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public bool IsSetAsDayOff { get; set; }
    public StartEndTime? StartEndTime { get; set; }
}