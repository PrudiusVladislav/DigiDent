using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Shared.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Employees.Shared.Abstractions;

public interface IEmployee<TId>: 
    IPerson<TId>
    where TId : IEmployeeId
{
    EmployeeStatus Status { get; }
    ICollection<WorkingDay> WorkingDays { get; }
    ICollection<SchedulePreference> SchedulePreferences { get; }
}