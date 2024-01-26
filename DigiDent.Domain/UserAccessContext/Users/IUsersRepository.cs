using DigiDent.Domain.SharedKernel.ValueObjects;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Domain.UserAccessContext.Users;

public interface IUsersRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
    Task DeleteAsync(UserId userId, CancellationToken cancellationToken);
}