using DigiDent.Domain.UserAccessContext.Roles;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Domain.UserAccessContext.Users;

public interface IUsersRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(Email userName, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
}