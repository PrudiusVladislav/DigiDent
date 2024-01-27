using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.UserAccessContext.Users;

public interface IUsersUnitOfWork: IUnitOfWork
{
    IUsersRepository UsersRepository { get; }
}