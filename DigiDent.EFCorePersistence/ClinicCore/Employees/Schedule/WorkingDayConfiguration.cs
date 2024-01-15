using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.EFCorePersistence.ClinicCore.SharedConfigurations;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees.Schedule;

[ClinicCoreEntityConfiguration]
public class WorkingDayConfiguration
    : BaseEntityConfiguration<WorkingDayId, Guid, WorkingDay>
{
    protected override void ConfigureEntity(EntityTypeBuilder<WorkingDay> builder)
    {
        builder
            .Property(wd => wd.StartEndTime)
            .HasConversion(SharedConverters.StartEndTimeConverter);
        
        

    }
}