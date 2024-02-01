using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.ClinicManagement.EFCorePersistence.Visits.Repositories;

public class ProvidedServicesRepository: IProvidedServicesRepository
{
    private readonly ClinicManagementDbContext _dbContext;
    
    public ProvidedServicesRepository(ClinicManagementDbContext dbContext)
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