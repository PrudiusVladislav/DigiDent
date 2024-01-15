using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.EFCorePersistence.ClinicCore.SharedConfigurations;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Visits;

[ClinicCoreEntityConfiguration]
public class DentalProcedureConfiguration
    : BaseEntityConfiguration<DentalProcedureId, Guid, DentalProcedure>
{
    protected override void ConfigureEntity(EntityTypeBuilder<DentalProcedure> builder)
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