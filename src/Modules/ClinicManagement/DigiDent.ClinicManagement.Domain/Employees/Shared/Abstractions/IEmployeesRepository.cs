using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;

namespace DigiDent.ClinicManagement.Domain.Employees.Shared.Abstractions;

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