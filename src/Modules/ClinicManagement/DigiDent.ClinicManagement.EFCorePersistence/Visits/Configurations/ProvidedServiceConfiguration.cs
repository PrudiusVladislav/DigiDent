using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.ClinicManagement.EFCorePersistence.Shared.Configurations;
using DigiDent.Shared.Infrastructure.Persistence.EfCore.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.ClinicManagement.EFCorePersistence.Visits.Configurations;

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