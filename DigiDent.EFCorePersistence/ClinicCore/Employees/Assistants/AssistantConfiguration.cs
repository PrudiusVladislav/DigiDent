using DigiDent.Domain.ClinicCoreContext.Employees.Assistants;
using DigiDent.Domain.ClinicCoreContext.Employees.Assistants.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.EFCorePersistence.ClinicCore.SharedConfigurations;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees.Assistants;

// [ClinicCoreEntityConfiguration]
public class AssistantConfiguration
    : AggregateRootConfiguration<EmployeeId, Guid, Assistant>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Assistant> builder)
    {
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<Assistant> builder)
    {
    }
}