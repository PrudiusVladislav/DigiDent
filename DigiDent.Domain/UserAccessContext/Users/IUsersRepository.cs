using DigiDent.Domain.UserAccessContext.Roles;

namespace DigiDent.Domain.UserAccessContext.Roles;

public interface IUsersRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(Email userName, CancellationToken cancellationToken);
}