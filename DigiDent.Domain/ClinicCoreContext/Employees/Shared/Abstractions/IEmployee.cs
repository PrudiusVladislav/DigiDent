using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;

public interface IEmployee<TId, TIdValue>: 
    IPerson<TId, TIdValue>
    where TId : IEmployeeId<TIdValue>
    where TIdValue : notnull
{
    EmployeeStatus Status { get; }
    ICollection<WorkingDay> WorkingDays { get; }
    ICollection<SchedulePreference> SchedulePreferences { get; }
}