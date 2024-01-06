using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Roles;

namespace DigiDent.Domain.UserAccessContext.Permissions;

public class Permission: IEntity<PermissionId, int>
{
    public PermissionId Id { get; init; }
    public string Name { get; private set; }
    
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    
    public static Permission CreateFromPermission(Permissions permissionName)
    {
        var permission = new Permission
        {
            Name = permissionName.ToString()
        };
        return permission;
    }
}