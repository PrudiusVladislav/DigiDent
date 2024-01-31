using DigiDent.Shared.Domain.Abstractions;

namespace DigiDent.UserAccess.Domain.Users.Abstractions;

public interface IUsersUnitOfWork: IUnitOfWork
{
    IUsersRepository UsersRepository { get; }
}