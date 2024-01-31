using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees.Doctors;

public class DoctorsRepository : 
    EmployeesRepository<Doctor>,
    IDoctorsRepository
{
    private readonly ClinicCoreDbContext _context;
    
    public DoctorsRepository(ClinicCoreDbContext context)
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
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        
        return await _context.Doctors
            .Include(d => d.Appointments)
            .Include(d => d.PastVisits)
            .Include(d => d.ProvidedServices)
            .Include(d => d.WorkingDays)
            .Include(d => d.SchedulePreferences)
            .SingleOrDefaultAsync(d => d.Id == id, cancellationToken);
    }
}