using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.SignUp;

public class SignUpCommandHandler
    : IRequestHandler<SignUpCommand, Result<AuthenticationResponse>> 
{
    private readonly IUsersRepository _usersRepository;
    private readonly UsersDomainService _usersDomainService;
    private readonly IJwtService _jwtService;
    
    public SignUpCommandHandler(
        IUsersRepository usersRepository,
        UsersDomainService usersDomainService,
        IJwtService jwtService)
    {
        _usersRepository = usersRepository;
        _usersDomainService = usersDomainService;
        _jwtService = jwtService;
    }
    
    public async Task<Result<AuthenticationResponse>> Handle(
        SignUpCommand request,
        CancellationToken cancellationToken)
    {
        var emailResult = await _usersDomainService.CreateEmailAsync(
            request.Email, cancellationToken);
        var passwordResult = Password.Create(request.Password);
        var fullNameResult = FullName.Create(request.FirstName, request.LastName);
        var roleResult = _usersDomainService.CreateRole(request.Role);
        
        var validationResult = Result.Merge(
            emailResult, passwordResult, fullNameResult, roleResult);

        if (validationResult.IsFailure) 
            return validationResult.MapToType<AuthenticationResponse>();
        
        var userToAdd = User.Create(
            TypedId.New<UserId>(),
            fullNameResult.Value!,
            emailResult.Value!,
            passwordResult.Value!,
            roleResult.Value!);
        await _usersDomainService.AddUserAsync(userToAdd, cancellationToken);
        
        var tokensResponse = await _jwtService
            .GenerateAuthenticationResponseAsync(userToAdd, cancellationToken);
        return Result.Ok(tokensResponse);
    }
}