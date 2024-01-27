using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.EFCorePersistence.ClinicCore.Shared.Configurations;
using DigiDent.EFCorePersistence.Shared.Configurations;
using DigiDent.EFCorePersistence.Shared.Converters;
using Microsoft.EntityFrameworkCore;
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
        
        builder
            .HasMany(a => a.ProvidedServices)
            .WithMany()
            .UsingEntity(j => j.ToTable("AppointmentsProvidedServices"));
        
        builder
            .HasIndex(a => a.DoctorId, "IX_Appointments_DoctorId");
        
        builder
            .HasIndex(a => a.PatientId, "IX_Appointments_PatientId");
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<Appointment> builder)
    {
    }
}