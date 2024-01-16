using DigiDent.Domain.SharedKernel.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.Shared;

/// <summary>
/// Base class for aggregate root configuration.
/// </summary>
/// <typeparam name="TId">The type of the entity id.</typeparam>
/// <typeparam name="TIdValue">The primitive type of the entity id value.</typeparam>
/// <typeparam name="TAggregateRoot">The type of the aggregate root instance.</typeparam>
public abstract class AggregateRootConfiguration<TAggregateRoot, TId, TIdValue>
    : BaseEntityConfiguration<TAggregateRoot, TId, TIdValue>
    where TAggregateRoot : class, IAggregateRoot, IEntity<TId, TIdValue>
    where TId : ITypedId<TIdValue>
    where TIdValue : notnull
    
{
    public override void Configure(EntityTypeBuilder<TAggregateRoot> builder)
    {
        base.Configure(builder);
        builder.Ignore(e => e.DomainEvents);

        ConfigureAggregateRoot(builder); 
    }

    /// <summary>
    /// Must be implemented in the derived classes to apply the aggregate root specific configuration.
    /// </summary>
    protected abstract void ConfigureAggregateRoot(EntityTypeBuilder<TAggregateRoot> builder);
}