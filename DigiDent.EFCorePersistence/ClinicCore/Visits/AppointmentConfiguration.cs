using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.EFCorePersistence.ClinicCore.Shared.Configurations;
using DigiDent.EFCorePersistence.Shared.Configurations;
using DigiDent.EFCorePersistence.Shared.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Visits;

[ClinicCoreEntityConfiguration]
public class AppointmentConfiguration
    : AggregateRootConfiguration<Appointment, AppointmentId, Guid>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Appointment> builder)
    {
        builder
            .Property(a => a.Duration)
            .HasConversion(SharedConverters.TimeDurationConverter);

        builder
            .Property(a => a.Status)
            .HasConversion(EnumerationsConverter
                .EnumToStringConverter<AppointmentStatus>());
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<Appointment> builder)
    {
    }
}