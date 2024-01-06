using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Roles;

namespace DigiDent.Domain.UserAccessContext.Permissions;

public class Permission: IEntity<PermissionId, int>
{
    public PermissionId Id { get; init; }
    public string Name { get; private set; }
    
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    
    public static Result<Permission> CreateFromPermission(string value)
    {
        var isPermissionValid = Enum.TryParse<Permissions>(
            value,
            true,
            out Permissions validPermissionName);
        
        if (!isPermissionValid)
            return Result.Fail<Permission>(PermissionErrors.PermissionIsNotValid(value));
        
        var permission = new Permission
        {
            Id = new PermissionId((int)validPermissionName),
            Name = validPermissionName.ToString()
        };
        return Result.Ok(permission);
    }
}