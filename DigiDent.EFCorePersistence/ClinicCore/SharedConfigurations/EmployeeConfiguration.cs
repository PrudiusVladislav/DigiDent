using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.SharedConfigurations;

public class EmployeeConfiguration<TId, TIdValue, TEmployeeEntity>
    : PersonConfiguration<TId, TIdValue, TEmployeeEntity>
    where TId : IEmployeeId<TIdValue>
    where TIdValue : notnull
    where TEmployeeEntity: class, IEmployee<TId, TIdValue>, IAggregateRoot, IEntity<TId, TIdValue>
{
    protected override void ConfigureEntity(EntityTypeBuilder<TEmployeeEntity> builder)
    {
        base.ConfigureEntity(builder);
        
        builder
            .Property(e => e.Status)
            .HasConversion(EnumerationsConverters
                .EnumToStringConverter<EmployeeStatus>());

        builder
            .HasMany(e => e.WorkingDays)
            .WithOne()
            .HasForeignKey(wd => wd.EmployeeId);
        
        builder
            .HasMany(e => e.SchedulePreferences)
            .WithOne()
            .HasForeignKey(sp => sp.EmployeeId);
    }
}