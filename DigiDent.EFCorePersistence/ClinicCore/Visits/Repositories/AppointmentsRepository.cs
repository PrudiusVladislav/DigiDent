using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.ClinicCore.Visits.Repositories;

public class AppointmentsRepository : 
    VisitsRepository<Appointment, AppointmentId, Guid>,
    IAppointmentsRepository
{
    private readonly ClinicCoreDbContext _context;
    public AppointmentsRepository(ClinicCoreDbContext context) 
        : base(context)
    {
        _context = context;
    }

    public async Task DeleteAsync(AppointmentId id, CancellationToken cancellationToken)
    {
        var appointment = await _context.Appointments
            .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
        if (appointment is null) return;
        
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync(cancellationToken);
    }
}