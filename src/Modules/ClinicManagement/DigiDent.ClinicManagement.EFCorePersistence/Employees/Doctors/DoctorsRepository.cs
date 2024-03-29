﻿using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.ClinicManagement.EFCorePersistence.Employees.Doctors;

public class DoctorsRepository : 
    EmployeesRepository<Doctor>,
    IDoctorsRepository
{
    private readonly ClinicManagementDbContext _context;
    
    public DoctorsRepository(ClinicManagementDbContext context)
        : base(context)
    {
        _context = context;
    }

    public override async Task<Doctor?> GetByIdAsync(
        EmployeeId id,
        CancellationToken cancellationToken,
        bool includeScheduling = false)
    {
        if (!includeScheduling)
            return await _context.Doctors
                .FindAsync(id, cancellationToken);
        
        return await _context.Doctors
            .Include(d => d.Appointments)
            .Include(d => d.WorkingDays)
            .Include(d => d.SchedulePreferences)
            .SingleOrDefaultAsync(d => d.Id == id, cancellationToken);
    }
}