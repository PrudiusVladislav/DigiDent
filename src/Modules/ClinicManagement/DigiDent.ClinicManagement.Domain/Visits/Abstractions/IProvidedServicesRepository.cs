﻿using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;

namespace DigiDent.ClinicManagement.Domain.Visits.Abstractions;

public interface IProvidedServicesRepository
{
    Task<IReadOnlyCollection<ProvidedService>> GetAllAsync(
        CancellationToken cancellationToken);
    
    Task<ProvidedService?> GetByIdAsync(
        ProvidedServiceId id, CancellationToken cancellationToken);
    
    Task<IReadOnlyCollection<ProvidedService>> GetAllFromIdsAsync(
        IEnumerable<ProvidedServiceId> ids, CancellationToken cancellationToken);
    
    Task AddAsync(ProvidedService providedService, CancellationToken cancellationToken);
    
    Task UpdateAsync(ProvidedService providedService, CancellationToken cancellationToken);
}