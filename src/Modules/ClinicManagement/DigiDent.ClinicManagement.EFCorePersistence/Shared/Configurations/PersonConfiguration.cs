using DigiDent.ClinicManagement.Domain.Shared.Abstractions;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.Shared.Infrastructure.Persistence.EfCore.Configurations;
using DigiDent.Shared.Infrastructure.Persistence.EfCore.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.ClinicManagement.EFCorePersistence.Shared.Configurations;

/// <summary>
/// Contains the default configuration for entities that implement <see cref="IPerson{TId}"/>.
/// </summary>
/// <typeparam name="TId"> The type of the person id. </typeparam>
/// <typeparam name="TPersonEntity">The person type. Should implement <see cref="IPerson{TId}"/>.</typeparam>
public class PersonConfiguration<TPersonEntity, TId>
    : AggregateRootConfiguration<TPersonEntity, TId, Guid>
    where TPersonEntity : class, IPerson<TId>
    where TId : IPersonId
{
    protected override void ConfigureEntity(EntityTypeBuilder<TPersonEntity> builder)
    {
        builder
            .Property(p => p.FullName)
            .HasConversion(ValueObjectsConverters.FullNameConverter)
            .HasColumnName("Full Name");
        
        builder
            .Property(p => p.Email)
            .HasConversion(ValueObjectsConverters.EmailConverter);

        builder
            .Property(p => p.PhoneNumber)
            .HasConversion(ValueObjectsConverters.PhoneNumberConverter);
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<TPersonEntity> builder)
    {
    }
}