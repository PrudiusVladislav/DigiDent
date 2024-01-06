using DigiDent.Domain.UserAccessContext.Roles;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.UserAccess.Configurations;

public class RoleConfiguration: BaseEntityConfiguration<RoleId, int, Role>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Role> builder)
    {
        builder.HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity(je => je.ToTable("RolePermissions"));
        //TODO: add data seeding (permissions and roles) here
    }
}