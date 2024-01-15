using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.EFCorePersistence.ClinicCore.SharedConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Patients;

[ClinicCoreEntityConfiguration]
public class PatientConfiguration: PersonConfiguration<PatientId, Guid, Patient>
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