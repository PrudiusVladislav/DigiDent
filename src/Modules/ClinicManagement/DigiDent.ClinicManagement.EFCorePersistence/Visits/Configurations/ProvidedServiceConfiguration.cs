using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.EFCorePersistence.ClinicCore.Shared.Configurations;
using DigiDent.EFCorePersistence.Shared.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Visits.Configurations;

[ClinicCoreEntityConfiguration]
public class ProvidedServiceConfiguration
    : BaseEntityConfiguration<ProvidedService, ProvidedServiceId, Guid>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ProvidedService> builder)
    {
        builder.OwnsOne(
            procedure => procedure.Details, 
            details => 
            {
                details.Property(d => d.Name).HasColumnName("Name");
                details.Property(d => d.Description).HasColumnName("Description");
            });

        builder
            .Property(procedure => procedure.UsualDuration)
            .HasConversion(SharedConverters.TimeDurationConverter);

        builder
            .Property(procedure => procedure.Price)
            .HasConversion(SharedConverters.MoneyConverter);

    }
}