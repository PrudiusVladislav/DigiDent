using DigiDent.Domain.ClinicCoreContext.Employees.Administrators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees.Administrators;

[ClinicCoreEntityConfiguration]
public class AdministratorConfiguration
    : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
    }
}