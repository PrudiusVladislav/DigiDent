using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Enumerations;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.ClinicManagement.EFCorePersistence.Constants;
using DigiDent.ClinicManagement.EFCorePersistence.Shared.Configurations;
using DigiDent.Shared.Infrastructure.EfCore.Configurations;
using DigiDent.Shared.Infrastructure.EfCore.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.ClinicManagement.EFCorePersistence.Visits.Configurations;

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
            .UsingEntity(j => j.ToTable(ConfigurationConstants
                .AppointmentsServicesJoinTableName));
        
        builder
            .Property(a => a.VisitDateTime)
            .HasConversion(SharedConverters.VisitDateTimeConverter);
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<Appointment> builder)
    {
    }
}