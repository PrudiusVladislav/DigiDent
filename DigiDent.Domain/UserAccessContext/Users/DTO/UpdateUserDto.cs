using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Domain.UserAccessContext.Users.DTO;

public class UpdateUserDto
{
    public UserId Id { get; set; }
    public FullName? FullName { get; set; }
    public Email Email { get; set; }
    public Password? Password { get; set; }
    public Role? Role { get; set; }

    private UpdateUserDto(User user)
    {
        Id = user.Id;
        FullName = user.FullName;
        Email = user.Email;
        Password = user.Password;
        Role = user.Role;
    }
    
    public static UpdateUserDto CopyFromUser(User user)
    {
        return new UpdateUserDto(user);
    }
}