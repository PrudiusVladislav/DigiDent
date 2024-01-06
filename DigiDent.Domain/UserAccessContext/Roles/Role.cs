using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Permissions;
using DigiDent.Domain.UserAccessContext.Roles;

namespace DigiDent.Domain.UserAccessContext.Roles;

public class Role: IEntity<RoleId, int>
{
    public RoleId Id { get; init; }
    public string Name { get; private set; }
    
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    
    public static Result<Role> Create(string value)
    {
        var isRoleValid = Enum.TryParse<Roles>(
            value,
            true,
            out Roles validRoleName);
        
        if (!isRoleValid)
            return Result.Fail<Role>(RoleErrors.RoleIsNotValid(value));
        
        var role = new Role
        {
            Id = new RoleId((int)validRoleName),
            Name = validRoleName.ToString()
        };
        return Result.Ok(role);
    }
}