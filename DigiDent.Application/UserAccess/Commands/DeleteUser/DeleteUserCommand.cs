using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.UserAccess.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) 
    : ICommand<Result>;