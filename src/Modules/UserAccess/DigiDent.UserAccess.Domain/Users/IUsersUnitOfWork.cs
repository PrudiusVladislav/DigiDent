using DigiDent.Shared.Domain.Abstractions;

namespace DigiDent.Domain.UserAccessContext.Users;

public interface IUsersUnitOfWork: IUnitOfWork
{
    IUsersRepository UsersRepository { get; }
}