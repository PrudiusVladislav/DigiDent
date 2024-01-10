using DigiDent.Domain.SharedKernel;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) 
    : IRequest<Result>;