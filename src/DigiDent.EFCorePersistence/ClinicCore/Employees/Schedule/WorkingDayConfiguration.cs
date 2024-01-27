using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.EFCorePersistence.ClinicCore.Shared.Configurations;
using DigiDent.EFCorePersistence.Shared.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees.Schedule;

[ClinicCoreEntityConfiguration]
public class WorkingDayConfiguration
    : BaseEntityConfiguration<WorkingDay, WorkingDayId, Guid>
{
    protected override void ConfigureEntity(EntityTypeBuilder<WorkingDay> builder)
    {
        builder
            .Property(wd => wd.StartEndTime)
            .HasConversion(SharedConverters
                .JsonSerializeConverter<StartEndTime>());

        builder
            .HasIndex(wd => wd.EmployeeId);
    }
}