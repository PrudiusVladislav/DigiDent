using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.EFCorePersistence.ClinicCore.SharedConfigurations;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees.Schedule;

[ClinicCoreEntityConfiguration]
public class SchedulePreferenceConfiguration
    : BaseEntityConfiguration<SchedulePreferenceId, Guid, SchedulePreference>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SchedulePreference> builder)
    {
        builder
            .Property(sp => sp.StartEndTime)
            .HasConversion(SharedConverters
                .JsonSerializeConverter<StartEndTime?>());
    }
}