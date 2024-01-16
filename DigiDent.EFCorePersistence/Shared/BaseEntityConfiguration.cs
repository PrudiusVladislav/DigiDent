using DigiDent.Domain.SharedKernel.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.Shared;

/// <summary>
/// Base class for entity configuration.
/// </summary>
/// <typeparam name="TId">The type of the entity id.</typeparam>
/// <typeparam name="TIdValue">The primitive type of the entity id value.</typeparam>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public abstract class BaseEntityConfiguration<TEntity, TId, TIdValue>
    : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity<TId, TIdValue>
    where TId : ITypedId<TIdValue>
    where TIdValue : notnull
    
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id.Value,
            value => TypedId.Create<TIdValue, TId>(value));

        ConfigureEntity(builder);
    }

    /// <summary>
    /// Must be implemented in the derived classes to configure the entity.
    /// </summary>
    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}