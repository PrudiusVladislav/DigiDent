using DigiDent.ClinicManagement.Domain.Patients;
using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.ClinicManagement.EFCorePersistence.Patients;

public class PatientsRepository: IPatientsRepository
{
    private readonly ClinicCoreDbContext _context;

    public PatientsRepository(ClinicCoreDbContext context)
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
            .ThenInclude(a => a.Doctor)
            .Include(p => p.PastVisits)
            .ThenInclude(pv => pv.Doctor)
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