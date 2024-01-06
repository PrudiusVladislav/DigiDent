using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Users.Enumerations;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Domain.UserAccessContext.Users;

public class Role: IEntity<RoleId, int>
{
    public RoleId Id { get; init; }
    public string Name { get; private set; }
    
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    
    public static Role Create(Roles roleName)
    {
        var role = new Role
        {
            Name = roleName.ToString()
        };
        return role;
    }
}