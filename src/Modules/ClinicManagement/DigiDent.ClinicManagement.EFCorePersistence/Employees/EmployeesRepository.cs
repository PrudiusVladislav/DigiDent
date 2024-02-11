using DigiDent.ClinicManagement.Domain.Employees.Shared;
using DigiDent.ClinicManagement.Domain.Employees.Shared.Abstractions;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.ClinicManagement.EFCorePersistence.Employees;

public abstract class EmployeesRepository<TEmployee>
    : IEmployeesRepository<TEmployee>
    where TEmployee : Employee
{
    private readonly ClinicManagementDbContext _context;
    
    protected EmployeesRepository(ClinicManagementDbContext context)
    {
        _context = context;
    }
    
    public async Task<IReadOnlyCollection<TEmployee>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return (await _context.Set<TEmployee>()
            .ToListAsync(cancellationToken))
            .AsReadOnly();
    }

    public virtual async Task<TEmployee?> GetByIdAsync(
        EmployeeId id,
        CancellationToken cancellationToken,
        bool includeScheduling = false)
    {
        if (!includeScheduling)
            return await _context.Set<TEmployee>()
                .FindAsync(id, cancellationToken);
        
        return await _context.Set<TEmployee>()
            .Include(e => e.WorkingDays)
            .Include(e => e.SchedulePreferences)
            .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(
        TEmployee employee, CancellationToken cancellationToken)
    {
        _context.Set<TEmployee>().Update(employee);
        await _context.SaveChangesAsync(cancellationToken); 
    }
}