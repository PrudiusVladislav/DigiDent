using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.SharedConfigurations;

/// <summary>
/// Contains the default configuration for entities that implement <see cref="IEmployee{TId,TIdValue}"/>.
/// </summary>
/// <typeparam name="TId"> The type of the employee id. </typeparam>
/// <typeparam name="TIdValue"> The primitive type of the employee id value. </typeparam>
/// <typeparam name="TEmployeeEntity">The employee type. Should implement <see cref="IEmployee{TId,TIdValue}"/>. </typeparam>
public class EmployeeConfiguration<TId, TIdValue, TEmployeeEntity>
    : PersonConfiguration<TId, TIdValue, TEmployeeEntity>
    where TId : IEmployeeId<TIdValue>
    where TIdValue : notnull
    where TEmployeeEntity: class, IEmployee<TId, TIdValue>
{
    protected override void ConfigureEntity(EntityTypeBuilder<TEmployeeEntity> builder)
    {
        base.ConfigureEntity(builder);
        
        builder
            .Property(e => e.Status)
            .HasConversion(EnumerationsConverter
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