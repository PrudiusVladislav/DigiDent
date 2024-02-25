namespace DigiDent.InventoryManagement.Domain.Employees.Repositories;

public interface IEmployeesCommandsRepository
{
    Task<Employee?> GetByIdAsync(EmployeeId id, CancellationToken cancellationToken);
    Task AddAsync(Employee employee, CancellationToken cancellationToken);
    Task UpdateAsync(Employee employee, CancellationToken cancellationToken);
}