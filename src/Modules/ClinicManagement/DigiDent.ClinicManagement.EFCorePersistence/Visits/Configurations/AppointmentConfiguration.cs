using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.EFCorePersistence.ClinicCore.Shared.Configurations;
using DigiDent.Shared.EFCorePersistence.Configurations;
using DigiDent.Shared.EFCorePersistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Visits.Configurations;

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
        
        builder
            .HasMany(a => a.ProvidedServices)
            .WithMany()
            .UsingEntity(j => j.ToTable("AppointmentsProvidedServices"));
        
        builder
            .Property(a => a.VisitDateTime)
            .HasConversion(SharedConverters.VisitDateTimeConverter);
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<Appointment> builder)
    {
    }
}