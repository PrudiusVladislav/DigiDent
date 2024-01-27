using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.EFCorePersistence.ClinicCore.Shared.Configurations;
using DigiDent.EFCorePersistence.Shared.Configurations;
using DigiDent.EFCorePersistence.Shared.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Visits;

[ClinicCoreEntityConfiguration]
public class PastVisitConfiguration
    : AggregateRootConfiguration<PastVisit, PastVisitId, Guid>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PastVisit> builder)
    {
        builder
            .Property(pv => pv.ProceduresDone)
            .HasConversion(SharedConverters
                .JsonSerializeConverter<IEnumerable<string>>())
            .Metadata.SetValueComparer(
                new ListComparer<string>(
                    new ValueComparer<string>(false)));

        builder
            .Property(pv => pv.PricePaid)
            .HasConversion(SharedConverters.MoneyConverter);
        
        builder
            .OwnsOne(pv => pv.Feedback, feedback =>
            {
                feedback
                    .Property(f => f.Rating)
                    .HasConversion(EnumerationsConverter
                        .EnumToStringConverter<FeedbackRating>())
                    .HasColumnName("Feedback_Rating");
                
                feedback
                    .Property(f => f.Comment)
                    .HasColumnName("Feedback_Comment");
            });
        
        builder
            .HasIndex(a => a.DoctorId, "IX_PastVisits_DoctorId");
        
        builder
            .HasIndex(a => a.PatientId, "IX_PastVisits_PatientId");
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<PastVisit> builder)
    {
    }
}