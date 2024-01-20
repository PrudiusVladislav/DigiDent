using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;

public interface IEmployeesRepository<TEmployee>
    where TEmployee: Employee 
{
    Task<IReadOnlyCollection<TEmployee>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEmployee?> GetByIdAsync(
        EmployeeId id,
        CancellationToken cancellationToken,
        bool includeScheduling = false);
    Task UpdateAsync(TEmployee employee, CancellationToken cancellationToken);
}