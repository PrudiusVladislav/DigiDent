﻿using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees;

public abstract class EmployeesRepository<TEmployee>
    : IEmployeesRepository<TEmployee>
    where TEmployee : Employee
{
    private readonly ClinicCoreDbContext _context;
    
    protected EmployeesRepository(ClinicCoreDbContext context)
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
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        
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