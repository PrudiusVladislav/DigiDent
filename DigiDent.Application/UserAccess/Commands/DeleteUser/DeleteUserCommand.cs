using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) 
    : IRequest<Result>;