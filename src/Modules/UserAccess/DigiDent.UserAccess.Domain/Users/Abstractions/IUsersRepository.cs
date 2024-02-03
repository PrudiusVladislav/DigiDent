using DigiDent.UserAccess.Domain.Users.ValueObjects;
using Email = DigiDent.Shared.Kernel.ValueObjects.Email;

namespace DigiDent.UserAccess.Domain.Users.Abstractions;

public interface IUsersRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
    Task DeleteAsync(UserId userId, CancellationToken cancellationToken);
}