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

    public override async Task<Appointment?> GetByIdAsync(
        AppointmentId id, CancellationToken cancellationToken)
    {
        return await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.TreatmentPlan)
            .Include(a => a.ProvidedServices)
            .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
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