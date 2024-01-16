using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.SharedConfigurations;

/// <summary>
/// Contains the default configuration for entities that implement <see cref="IPerson{TId,TIdValue}"/>.
/// </summary>
/// <typeparam name="TId"> The type of the person id. </typeparam>
/// <typeparam name="TIdValue"> The primitive type of the person id value. </typeparam>
/// <typeparam name="TPersonEntity">The person type. Should implement <see cref="IPerson{TId,TIdValue}"/>. </typeparam>
public class PersonConfiguration<TPersonEntity, TId, TIdValue>
    : AggregateRootConfiguration<TPersonEntity, TId, TIdValue>
    where TPersonEntity : class, IPerson<TId, TIdValue>
    where TId : IPersonId<TIdValue>
    where TIdValue : notnull
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

        builder
            .Property(p => p.Gender)
            .HasConversion(EnumerationsConverter
                .EnumToStringConverter<Gender>());
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<TPersonEntity> builder)
    {
    }
}