using DigiDent.Domain.ClinicCoreContext.Employees.Assistants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees.Assistants;

[ClinicCoreEntityConfiguration]
public class AssistantConfiguration
    : IEntityTypeConfiguration<Assistant>
{
    public void Configure(EntityTypeBuilder<Assistant> builder)
    {
    }
}