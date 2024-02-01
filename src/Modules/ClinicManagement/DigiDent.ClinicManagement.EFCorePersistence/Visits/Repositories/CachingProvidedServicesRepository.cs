using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.ClinicManagement.EFCorePersistence.Shared;
using Microsoft.Extensions.Caching.Memory;

namespace DigiDent.ClinicManagement.EFCorePersistence.Visits.Repositories;

public class CachingProvidedServicesRepository: IProvidedServicesRepository
{
    private readonly IProvidedServicesRepository _providedServicesRepository;
    private readonly IMemoryCache _memoryCache;
    
    public CachingProvidedServicesRepository(
        IProvidedServicesRepository providedServicesRepository,
        IMemoryCache memoryCache)
    {
        _providedServicesRepository = providedServicesRepository;
        _memoryCache = memoryCache;
    }
    
    public Task<IReadOnlyCollection<ProvidedService>> GetAllAsync(
        CancellationToken cancellationToken)
    {
       return _memoryCache.GetOrCreateAsync(
           CacheKeys.ProvidedServicesAll, 
           entry => 
           {
               entry.AbsoluteExpirationRelativeToNow = CacheConstants.CacheExpiration;
               return _providedServicesRepository.GetAllAsync(cancellationToken);
           })!;
    }

    public Task<ProvidedService?> GetByIdAsync(
        ProvidedServiceId id, CancellationToken cancellationToken)
    {
        return _memoryCache.GetOrCreateAsync(
            CacheKeys.ProvidedServiceById(id), 
            entry => 
            {
                entry.AbsoluteExpirationRelativeToNow = CacheConstants.CacheExpiration;
                return _providedServicesRepository.GetByIdAsync(id, cancellationToken);
            });
    }

    public Task<IReadOnlyCollection<ProvidedService>> GetAllFromIdsAsync(
        IEnumerable<ProvidedServiceId> ids, CancellationToken cancellationToken)
    {
        return _providedServicesRepository.GetAllFromIdsAsync(ids, cancellationToken);
    }

    public Task AddAsync(ProvidedService providedService, CancellationToken cancellationToken)
    {
        return _providedServicesRepository.AddAsync(providedService, cancellationToken)
            .ContinueWith(task =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    _memoryCache.Set(
                        CacheKeys.ProvidedServiceById(providedService.Id),
                        providedService,
                        CacheConstants.DefaultCacheOptions);
                }
                return task;
            }, cancellationToken);
    }

    public Task UpdateAsync(ProvidedService providedService, CancellationToken cancellationToken)
    {
        return _providedServicesRepository.UpdateAsync(providedService, cancellationToken)
            .ContinueWith(task =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    _memoryCache.Set(
                        CacheKeys.ProvidedServiceById(providedService.Id),
                        providedService,
                        CacheConstants.DefaultCacheOptions);
                }
                return task;
            }, cancellationToken);
    }
}