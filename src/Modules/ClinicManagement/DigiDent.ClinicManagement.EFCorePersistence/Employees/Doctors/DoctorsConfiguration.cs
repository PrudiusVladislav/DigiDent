using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Employees.Doctors.ValueObjects;
using DigiDent.ClinicManagement.EFCorePersistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.ClinicManagement.EFCorePersistence.Employees.Doctors;

public class DoctorsConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder
            .HasMany(d => d.ProvidedServices)
            .WithMany(dp => dp.Doctors)
            .UsingEntity(je => je.ToTable(ConfigurationConstants
                .DoctorsServicesJoinTableName));
        
        builder
            .HasMany(d => d.Appointments)
            .WithOne(a => a.Doctor)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasMany(d => d.PastVisits)
            .WithOne(pv => pv.Doctor)
            .HasForeignKey(pv => pv.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}