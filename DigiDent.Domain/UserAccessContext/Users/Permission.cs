using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Users.Enumerations;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Domain.UserAccessContext.Users;

public class Permission: IEntity<PermissionId, int>
{
    public PermissionId Id { get; init; }
    public string Name { get; private set; }
    
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    
    public static Permission Create(Permissions permissionName)
    {
        var permission = new Permission
        {
            Name = permissionName.ToString()
        };
        return permission;
    }
}