using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.ClinicCore.Patients;

public class PatientRepository: IPatientRepository
{
    private readonly ClinicCoreDbContext _dbContext;
    
    public PatientRepository(ClinicCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IReadOnlyCollection<Patient>> GetAllAsync(CancellationToken cancellationToken)
    {
        return (await _dbContext.Patients
            .ToListAsync(cancellationToken))
            .AsReadOnly();
    }
    
    public new async Task<Patient?> GetByIdAsync(
        PatientId id, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Set<Patient>()
            .Include(p => p.Appointments)
            .ThenInclude(a => a.Doctor)
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public Task AddAsync(Patient entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Patient entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(PatientId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}