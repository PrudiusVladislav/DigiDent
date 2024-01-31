using DigiDent.UserAccess.Application.Abstractions;
using DigiDent.Shared.Application.Abstractions;
using DigiDent.Shared.Domain.Errors;
using DigiDent.Shared.Domain.ReturnTypes;
using DigiDent.UserAccess.Application.Commands.Shared;
using DigiDent.UserAccess.Domain.Users;
using DigiDent.UserAccess.Domain.Users.Abstractions;
using DigiDent.UserAccess.Domain.Users.Errors;
using DigiDent.UserAccess.Domain.Users.Services;

namespace DigiDent.UserAccess.Application.Commands.SignIn;

public sealed class SignInCommandHandler
    : ICommandHandler<SignInCommand, Result<AuthenticationResponse>>
{
    private readonly IJwtService _jwtService;
    private readonly UsersDomainService _usersDomainService;
    private readonly IUsersRepository _usersRepository;
    
    public SignInCommandHandler(
        IJwtService jwtService,
        UsersDomainService usersDomainService,
        IUsersRepository usersRepository)
    {
        _jwtService = jwtService;
        _usersDomainService = usersDomainService;
        _usersRepository = usersRepository;
    }
    
    public async Task<Result<AuthenticationResponse>> Handle(
        SignInCommand command, CancellationToken cancellationToken)
    {
        User? userToSignIn = await _usersRepository
            .GetByEmailAsync(command.Email, cancellationToken);
        
        if (userToSignIn == null)
        {
            return Result.Fail<AuthenticationResponse>(EmailErrors
                .EmailIsNotRegistered(command.Email.Value));
        }
        
        var isPasswordCorrect = userToSignIn.Password.IsEqualTo(command.Password);
        if (!isPasswordCorrect)
        {
            return Result.Fail<AuthenticationResponse>(PasswordErrors
                .PasswordDoesNotMatch);
        }
        
        AuthenticationResponse tokensResponse = await _jwtService
            .GenerateAuthenticationResponseAsync(userToSignIn, cancellationToken);
        return Result.Ok(tokensResponse);
        
        //TODO: Refactor the UsersDomainService, possible extract some methods to different service
        //https://stackoverflow.com/questions/17745937/ddd-domain-services-what-should-a-service-class-contain
        //https://dev.to/ruben_alapont/domain-services-and-factories-in-domain-driven-design-55lo
    }
}