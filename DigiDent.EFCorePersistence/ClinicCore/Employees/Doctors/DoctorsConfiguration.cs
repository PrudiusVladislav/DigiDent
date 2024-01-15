﻿using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors.ValueObjects;
using DigiDent.EFCorePersistence.ClinicCore.SharedConfigurations;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees.Doctors;

[ClinicCoreEntityConfiguration]
public class DoctorsConfiguration
    : EmployeeConfiguration<DoctorId, Guid, Doctor>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Doctor> builder)
    {
        base.ConfigureEntity(builder);

        builder
            .Property(d => d.Specialization)
            .HasConversion(EnumerationsConverter
                .EnumToStringConverter<DoctorSpecialization>());

        builder
            .HasMany(d => d.ProvidedServices)
            .WithMany(dp => dp.Doctors)
            .UsingEntity(je => je.ToTable("DoctorsProvidedServices"));
        
        builder
            .HasMany(d => d.Appointments)
            .WithOne(a => a.Doctor)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasMany(d => d.PastVisits)
            .WithOne()
            .HasForeignKey(pv => pv.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        //TODO: configure the relations and collections
    }
}