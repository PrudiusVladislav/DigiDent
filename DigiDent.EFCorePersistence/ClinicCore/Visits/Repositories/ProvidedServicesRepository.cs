using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.ClinicCore.Visits.Repositories;

public class ProvidedServicesRepository: IProvidedServicesRepository
{
    private readonly ClinicCoreDbContext _dbContext;
    
    public ProvidedServicesRepository(ClinicCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IReadOnlyCollection<ProvidedService>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return (await _dbContext.ProvidedServices
            .ToListAsync(cancellationToken))
            .AsReadOnly();
    }

    public async Task<ProvidedService?> GetByIdAsync(
        ProvidedServiceId id, CancellationToken cancellationToken)
    {
        return await _dbContext.ProvidedServices
            .Include(ps => ps.Doctors)
            .SingleOrDefaultAsync(
                providedService => providedService.Id == id, 
                cancellationToken);
    }
    
    public async Task<IReadOnlyCollection<ProvidedService>> GetAllFromIdsAsync(
        IEnumerable<ProvidedServiceId> ids, CancellationToken cancellationToken)
    {
        return (await _dbContext.ProvidedServices
            .Include(ps => ps.Doctors)
            .Where(providedService => ids.Contains(providedService.Id))
            .ToListAsync(cancellationToken))
            .AsReadOnly();
    }

    public async Task AddAsync(
        ProvidedService providedService, CancellationToken cancellationToken)
    {
        await _dbContext.ProvidedServices.AddAsync(
            providedService, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        ProvidedService providedService, CancellationToken cancellationToken)
    {
        _dbContext.ProvidedServices.Update(providedService);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}