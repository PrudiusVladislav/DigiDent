using DigiDent.ClinicManagement.Domain.Employees.Shared;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.EFCorePersistence.Shared.Configurations;
using DigiDent.Shared.Infrastructure.EfCore.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.ClinicManagement.EFCorePersistence.Employees;

/// <summary>
/// Contains the default configuration for entities that implement <see cref="Employee"/>.
/// </summary>
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