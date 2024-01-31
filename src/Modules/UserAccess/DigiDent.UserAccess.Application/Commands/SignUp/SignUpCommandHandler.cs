using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.UserAccess.Domain.Users;
using DigiDent.UserAccess.Domain.Users.Services;

namespace DigiDent.UserAccess.Application.Commands.SignUp;

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
        var isEmailUnique = await _usersDomainService.IsEmailUniqueAsync(
            request.Email, cancellationToken);
        
        if (!isEmailUnique)
            return Result.Fail(EmailErrors
                .EmailIsNotUnique(request.Email.Value));
        
        User userToAdd = new(
            request.FullName,
            request.Email,
            request.PhoneNumber,
            request.Password,
            request.Role);
        
        await _usersDomainService.AddUserAsync(userToAdd, cancellationToken);
        
        return Result.Ok();
    }
}