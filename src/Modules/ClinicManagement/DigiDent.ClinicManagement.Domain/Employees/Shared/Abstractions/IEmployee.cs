using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;

public interface IEmployee<TId>: 
    IPerson<TId>
    where TId : IEmployeeId
{
    EmployeeStatus Status { get; }
    ICollection<WorkingDay> WorkingDays { get; }
    ICollection<SchedulePreference> SchedulePreferences { get; }
}