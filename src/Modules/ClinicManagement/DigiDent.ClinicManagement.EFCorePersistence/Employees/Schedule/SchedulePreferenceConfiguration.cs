using DigiDent.ClinicManagement.Domain.Employees.Shared;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.EFCorePersistence.Shared.Configurations;
using DigiDent.Shared.Infrastructure.EfCore.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.ClinicManagement.EFCorePersistence.Employees.Schedule;

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