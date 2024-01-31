﻿using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Employees.Doctors.ValueObjects;
using DigiDent.Shared.Infrastructure.EfCore.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.ClinicManagement.EFCorePersistence.Employees.Doctors;

public class DoctorsConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
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
            .WithOne(pv => pv.Doctor)
            .HasForeignKey(pv => pv.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}