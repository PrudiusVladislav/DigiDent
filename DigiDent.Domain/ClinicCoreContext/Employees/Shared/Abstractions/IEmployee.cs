using DigiDent.Domain.ClinicCoreContext.Visits;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;

public interface IEmployee
{
    ICollection<WorkingDay> WorkingDays { get; }
}