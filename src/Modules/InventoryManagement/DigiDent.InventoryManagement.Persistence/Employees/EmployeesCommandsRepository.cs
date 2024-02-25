using DigiDent.InventoryManagement.Domain.Employees;

namespace DigiDent.InventoryManagement.Persistence.Employees;

public class EmployeesCommandsRepository: IEmployeesCommandsRepository
{
    private readonly InventoryManagementDbContext _dbContext;

    public EmployeesCommandsRepository(InventoryManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Employee?> GetByIdAsync(
        EmployeeId id, CancellationToken cancellationToken)
    {
        return await _dbContext.Employees.FindAsync(
            [id], cancellationToken: cancellationToken);
    }

    public async Task AddAsync(
        Employee employee, CancellationToken cancellationToken)
    {
        await _dbContext.Employees.AddAsync(employee, cancellationToken);
    }

    public async Task UpdateAsync(
        Employee employee, CancellationToken cancellationToken)
    {
        _dbContext.Employees.Update(employee);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}