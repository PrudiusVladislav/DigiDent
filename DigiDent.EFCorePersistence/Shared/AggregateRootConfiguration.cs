using DigiDent.Domain.SharedKernel.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.Shared;

public abstract class AggregateRootConfiguration<TId, TIdValue, TAggregateRoot>
    : BaseEntityConfiguration<TId, TIdValue, TAggregateRoot>
    where TId : ITypedId<TIdValue>
    where TIdValue : notnull
    where TAggregateRoot : class, IAggregateRoot, IEntity<TId, TIdValue>
{
    public override void Configure(EntityTypeBuilder<TAggregateRoot> builder)
    {
        base.Configure(builder);
        builder.Ignore(e => e.DomainEvents);

        ConfigureAggregateRoot(builder); 
    }

    protected abstract void ConfigureAggregateRoot(EntityTypeBuilder<TAggregateRoot> builder);
}