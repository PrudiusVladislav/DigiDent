using DigiDent.Domain.SharedKernel.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.Shared;

public abstract class BaseEntityConfiguration<TId, TIdValue, TEntity>
    : IEntityTypeConfiguration<TEntity>
    where TId : TypedId<TIdValue>
    where TIdValue : notnull
    where TEntity : class, IEntity<TId, TIdValue>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id.Value,
            value => TypedId<TIdValue>.Create<TId>(value));

        ConfigureEntity(builder);
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}