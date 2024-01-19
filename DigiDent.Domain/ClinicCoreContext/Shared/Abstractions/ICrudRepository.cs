using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

//TODO: basic implementation, add more methods as needed
public interface ICrudRepository<TEntity, in TId, TIdValue>
    where TEntity : IEntity<TId, TIdValue>
    where TId : ITypedId<TIdValue>
    where TIdValue : notnull
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TId id, CancellationToken cancellationToken);
}