using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;
using DigiDent.Domain.UserAccessContext.Users;
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
        
        var fullNameResult = FullName.Create(request.FirstName, request.LastName);
        var emailResult = await _usersDomainService.CreateEmailAsync(
            request.Email, cancellationToken);
        var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
        var passwordResult = Password.Create(request.Password);
        var roleResult = _usersDomainService.CreateRole(request.Role);
        
        var validationResult = Result.Merge(
            fullNameResult, emailResult, phoneNumberResult, passwordResult, roleResult);

        if (validationResult.IsFailure) return validationResult;
        
        var userToAdd = User.Create(
            TypedId.New<UserId>(),
            fullNameResult.Value!,
            emailResult.Value!,
            phoneNumberResult.Value!,
            passwordResult.Value!,
            roleResult.Value);
        await _usersDomainService.AddUserAsync(userToAdd, cancellationToken);
        
        return Result.Ok();
    }
}