using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.SharedConfigurations;

public class PersonConfiguration<TId, TIdValue, TPersonEntity>
    : AggregateRootConfiguration<TId, TIdValue, TPersonEntity>
    where TId : IPersonId<TIdValue>
    where TIdValue : notnull
    where TPersonEntity : class, IPerson, IAggregateRoot, IEntity<TId, TIdValue>
    
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
            .HasConversion(EnumerationsConverters
                .EnumToStringConverter<Gender>());
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<TPersonEntity> builder)
    {
    }
}