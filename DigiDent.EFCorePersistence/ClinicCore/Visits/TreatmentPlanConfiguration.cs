using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.EFCorePersistence.Shared.Configurations;
using DigiDent.EFCorePersistence.Shared.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Visits;

[ClinicCoreEntityConfiguration]
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
            .Property(plan => plan.Status)
            .HasConversion(EnumerationsConverter
                .EnumToStringConverter<TreatmentPlanStatus>());

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