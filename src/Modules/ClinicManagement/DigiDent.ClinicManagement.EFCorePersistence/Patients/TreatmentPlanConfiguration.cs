using DigiDent.ClinicManagement.Domain.Patients;
using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;
using DigiDent.Shared.Infrastructure.EfCore.Configurations;
using DigiDent.Shared.Infrastructure.EfCore.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.ClinicManagement.EFCorePersistence.Patients;

public class TreatmentPlanConfiguration
    : BaseEntityConfiguration<TreatmentPlan, TreatmentPlanId, Guid>
{
    protected override void ConfigureEntity(EntityTypeBuilder<TreatmentPlan> builder)
    {
        builder
            .Property(plan => plan.Details)
            .HasConversion(
                details => details.DiagnosisDescription,
                value => new TreatmentPlanDetails(value));

        builder
            .HasMany(plan => plan.Appointments)
            .WithOne(appointment => appointment.TreatmentPlan)
            .HasForeignKey(appointment => appointment.TreatmentPlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(plan => plan.PastVisits)
            .WithOne(visit => visit.TreatmentPlan)
            .HasForeignKey(visit => visit.TreatmentPlanId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}