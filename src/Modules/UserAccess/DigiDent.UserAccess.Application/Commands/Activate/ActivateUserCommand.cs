using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.UserAccess.Domain.Users.ValueObjects;

namespace DigiDent.UserAccess.Application.Commands.Activate;

public sealed record ActivateUserCommand: ICommand<Result>
{
    public UserId UserId { get; }

    public ActivateUserCommand(Guid userId)
    {
        UserId = new UserId(userId);
    }
}