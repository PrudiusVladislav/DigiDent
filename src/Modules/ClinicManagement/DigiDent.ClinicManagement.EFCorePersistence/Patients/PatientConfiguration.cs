﻿using DigiDent.ClinicManagement.Domain.Patients;
using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;
using DigiDent.ClinicManagement.EFCorePersistence.Shared.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.ClinicManagement.EFCorePersistence.Patients;

public class PatientConfiguration
    : PersonConfiguration<Patient, PatientId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Patient> builder)
    {
        base.ConfigureEntity(builder);
        
        builder
            .HasMany(patient => patient.Appointments)
            .WithOne(appointment => appointment.Patient)
            .HasForeignKey(appointment => appointment.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasMany(patient => patient.PastVisits)
            .WithOne(visit => visit.Patient)
            .HasForeignKey(visit => visit.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(patient => patient.TreatmentPlans)
            .WithOne(plan => plan.Patient)
            .HasForeignKey(plan => plan.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}