using DigiDent.Domain.UserAccessContext.Permissions;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.UserAccess.Configurations;

public class PermissionConfiguration: BaseEntityConfiguration<PermissionId, int, Permission>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Permission> builder)
    {
        throw new NotImplementedException();
    }
}