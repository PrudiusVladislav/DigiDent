using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.UserAccess.Domain.Users;
using DigiDent.UserAccess.Domain.Users.Abstractions;

namespace DigiDent.UserAccess.Application.Commands.Activate;

public class ActivateUserCommandHandler
    : ICommandHandler<ActivateUserCommand, Result>
{
    private readonly IUsersUnitOfWork _usersUnitOfWork;

    public ActivateUserCommandHandler(IUsersUnitOfWork usersUnitOfWork)
    {
        _usersUnitOfWork = usersUnitOfWork;
    }

    public async Task<Result> Handle(
        ActivateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _usersUnitOfWork.UsersRepository.GetByIdAsync(
            command.UserId, cancellationToken);
        
        if (user is null)
        {
            return Result.Fail(RepositoryErrors
                .EntityNotFound<User>(command.UserId.Value));
        }

        Result activationResult = user.Activate();
        
        if (activationResult.IsFailure)
            return activationResult;

        await _usersUnitOfWork.UsersRepository.UpdateAsync(user, cancellationToken);
        await _usersUnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }
}