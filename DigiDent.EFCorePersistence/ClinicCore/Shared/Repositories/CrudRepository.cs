using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.SharedKernel.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.ClinicCore.Shared.Repositories;

public class CrudRepository<TEntity, TId, TIdValue>
    : ICrudRepository<TEntity, TId, TIdValue>
    where TEntity : class, IEntity<TId, TIdValue>
    where TId : ITypedId<TIdValue>
    where TIdValue : notnull
{
    private readonly ClinicCoreDbContext _dbContext;

    public CrudRepository(ClinicCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Set<TEntity>()
            .FindAsync([id], cancellationToken);
    }

    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return (await _dbContext
            .Set<TEntity>()
            .ToListAsync(cancellationToken))
            .AsReadOnly();
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbContext
            .Set<TEntity>()
            .AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbContext
            .Set<TEntity>()
            .Update(entity);
    }

    public async Task DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null) return;
        
        _dbContext
            .Set<TEntity>()
            .Remove(entity);
    }
}