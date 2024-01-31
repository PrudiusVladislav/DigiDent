using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.EFCorePersistence.ClinicCore.Shared.Configurations;
using DigiDent.Shared.EFCorePersistence.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees.Schedule;

[ClinicCoreEntityConfiguration]
public class SchedulePreferenceConfiguration
    : BaseEntityConfiguration<SchedulePreference, SchedulePreferenceId, Guid>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SchedulePreference> builder)
    {
        builder
            .Property(sp => sp.StartEndTime)
            .HasConversion(SharedConverters
                .JsonSerializeConverter<StartEndTime?>());
    }
}