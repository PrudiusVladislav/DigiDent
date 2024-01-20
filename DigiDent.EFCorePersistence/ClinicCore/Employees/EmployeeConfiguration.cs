using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.EFCorePersistence.ClinicCore.Shared.Configurations;
using DigiDent.EFCorePersistence.Shared.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees;

/// <summary>
/// Contains the default configuration for entities that implement <see cref="Employee"/>.
/// </summary>
[ClinicCoreEntityConfiguration]
public class EmployeeConfiguration
    : PersonConfiguration<Employee, EmployeeId>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Employee> builder)
    {
        base.ConfigureEntity(builder);
        
        builder
            .Property(e => e.Status)
            .HasConversion(EnumerationsConverter
                .EnumToStringConverter<EmployeeStatus>());

        builder
            .HasMany(e => e.WorkingDays)
            .WithOne(wd => wd.Employee)
            .HasForeignKey(wd => wd.EmployeeId);
        
        builder
            .HasMany(e => e.SchedulePreferences)
            .WithOne(sp => sp.Employee)
            .HasForeignKey(sp => sp.EmployeeId);
    }
}