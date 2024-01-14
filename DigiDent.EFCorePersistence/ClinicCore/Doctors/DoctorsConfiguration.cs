using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors.ValueObjects;
using DigiDent.EFCorePersistence.ClinicCore.SharedConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Doctors;

[ClinicCoreEntityConfiguration]
public class DoctorsConfiguration
    : PersonConfiguration<DoctorId, Guid, Doctor>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Doctor> builder)
    {
        base.ConfigureEntity(builder);

        builder
            .Property(p => p.Specialization)
            .HasConversion(EnumerationsConverters
                .EnumToStringConverter<DoctorSpecialization>());

        builder
            .Property(p => p.Biography)
            .HasMaxLength(1000);
        
        //TODO: configure the relations and collections
    }
}