using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Enumerations;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.ClinicManagement.EFCorePersistence.Shared.Configurations;
using DigiDent.Shared.Infrastructure.Persistence.EfCore.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.ClinicManagement.EFCorePersistence.Visits.Configurations;

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
                    .HasColumnName("Feedback_Rating");
                
                feedback
                    .Property(f => f.Comment)
                    .HasColumnName("Feedback_Comment");
            });
        
        builder
            .Property(pv => pv.VisitDateTime)
            .HasConversion(SharedConverters.VisitDateTimeConverter);
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<PastVisit> builder)
    {
    }
}