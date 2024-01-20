using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.ClinicCore.Patients;

public class PatientsesRepository: IPatientsRepository
{
    private readonly ClinicCoreDbContext _context;

    public PatientsesRepository(ClinicCoreDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Patient>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return (await _context.Patients
            .ToListAsync(cancellationToken))
            .AsReadOnly();
    }

    public async Task<Patient?> GetByIdAsync(
        PatientId id, CancellationToken cancellationToken)
    {
        return await _context.Patients
            .Include(p => p.Appointments)
            .Include(p => p.PastVisits)
            .Include(p => p.TreatmentPlans)
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(
        Patient patient, CancellationToken cancellationToken)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync(cancellationToken);
    }
}