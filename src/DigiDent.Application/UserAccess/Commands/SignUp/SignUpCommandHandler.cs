using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.Errors;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Application.UserAccess.Commands.SignUp;

public class SignUpCommandHandler
    : ICommandHandler<SignUpCommand, Result> 
{
    private readonly UsersDomainService _usersDomainService;
    
    public SignUpCommandHandler(UsersDomainService usersDomainService)
    {
        _usersDomainService = usersDomainService;
    }
    
    public async Task<Result> Handle(
        SignUpCommand request,
        CancellationToken cancellationToken)
    {
        var isEmailUnique = await _usersDomainService.IsEmailUnique(
            request.Email, cancellationToken);
        
        if (!isEmailUnique)
            return Result.Fail(EmailErrors
                .EmailIsNotUnique(request.Email.Value));
        
        var userToAdd = User.Create(
            request.FullName,
            request.Email,
            request.PhoneNumber,
            request.Password,
            request.Role);
        
        await _usersDomainService.AddUserAsync(userToAdd, cancellationToken);
        
        return Result.Ok();
    }
}