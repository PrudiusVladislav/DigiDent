using DigiDent.ClinicManagement.Domain.Employees.Shared;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.EFCorePersistence.Shared.Configurations;
using DigiDent.Shared.Infrastructure.Persistence.EfCore.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.ClinicManagement.EFCorePersistence.Employees.Schedule;

public class WorkingDayConfiguration
    : BaseEntityConfiguration<WorkingDay, WorkingDayId, Guid>
{
    protected override void ConfigureEntity(EntityTypeBuilder<WorkingDay> builder)
    {
        builder
            .Property(wd => wd.StartEndTime)
            .HasConversion(SharedConverters
                .JsonSerializeConverter<StartEndTime>());
    }
}