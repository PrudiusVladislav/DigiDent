using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.DeleteUser;

public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand, Result>
{
    private readonly UsersDomainService _usersDomainService;
    
    public DeleteUserCommandHandler(UsersDomainService usersDomainService)
    {
        _usersDomainService = usersDomainService;
    }
    
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _usersDomainService.DeleteUserAsync(
            new UserId(request.UserId), cancellationToken);
    }
}