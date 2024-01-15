using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.EFCorePersistence.ClinicCore.SharedConfigurations;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Visits;

public class PastVisitConfiguration
    : BaseEntityConfiguration<VisitId, Guid, PastVisit>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PastVisit> builder)
    {
        builder
            .Property(pv => pv.ProceduresDone)
            .HasConversion(SharedConverters
                .JsonSerializeConverter<IEnumerable<string>>());

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
    }
}