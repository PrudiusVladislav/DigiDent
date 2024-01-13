using DigiDent.Domain.ClinicCoreContext.Visits;

namespace DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

public interface IEmployee
{
    ICollection<WorkingDay> WorkingDays { get; }
}