using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.UserAccess.Domain.Users.Abstractions;

public interface IUsersUnitOfWork: IUnitOfWork
{
    IUsersRepository UsersRepository { get; }
}