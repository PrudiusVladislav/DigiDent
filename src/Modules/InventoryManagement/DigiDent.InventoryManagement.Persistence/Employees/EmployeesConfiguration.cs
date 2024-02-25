using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.Shared.Infrastructure.Persistence.EfCore.Configurations;
using DigiDent.Shared.Infrastructure.Persistence.EfCore.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.InventoryManagement.Persistence.Employees;

public class EmployeesConfiguration
    : BaseEntityConfiguration<Employee, EmployeeId, Guid>
{
    protected override void ConfigureEntity(
        EntityTypeBuilder<Employee> builder)
    {
        builder
            .Property(employee => employee.Name)
            .HasConversion(ValueObjectsConverters.FullNameConverter);

        builder
            .Property(employee => employee.Email)
            .HasConversion(ValueObjectsConverters.EmailConverter);
        
        builder
            .Property(employee => employee.PhoneNumber)
            .HasConversion(ValueObjectsConverters.PhoneNumberConverter);
    }
}