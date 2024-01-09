using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.Errors;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.SignIn;

public class SignInCommandHandler
    : IRequestHandler<SignInCommand, Result<AuthenticationResponse>>
{
    private readonly IJwtService _jwtService;
    private readonly UsersDomainService _usersDomainService;
    private readonly IUsersRepository _usersRepository;
    private readonly IRefreshTokensRepository _refreshTokensRepository;
    
    public SignInCommandHandler(
        IJwtService jwtService,
        UsersDomainService usersDomainService,
        IUsersRepository usersRepository,
        IRefreshTokensRepository refreshTokensRepository)
    {
        _jwtService = jwtService;
        _usersDomainService = usersDomainService;
        _usersRepository = usersRepository;
        _refreshTokensRepository = refreshTokensRepository;
    }
    
    public async Task<Result<AuthenticationResponse>> Handle(
        SignInCommand request,
        CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
            return Result.Fail<AuthenticationResponse>(EmailErrors
                .EmailIsNotRegistered(request.Email));
        
        var userToSignIn = await _usersRepository
            .GetByEmailAsync(emailResult.Value!, cancellationToken);
        if (userToSignIn == null)
            return Result.Fail<AuthenticationResponse>(EmailErrors
                .EmailIsNotRegistered(request.Email));
        
        var roleResult = _usersDomainService.CreateRole(request.Role);
        if (roleResult.IsFailure || userToSignIn.Role != roleResult.Value)
            return Result.Fail<AuthenticationResponse>(RoleErrors
                .RoleIsNotValid(request.Role));
        
        var isPasswordCorrect = userToSignIn.Password.IsEqualTo(request.Password);
        if (!isPasswordCorrect)
            return Result.Fail<AuthenticationResponse>(PasswordErrors
                .PasswordDoesNotMatch);
        
        var tokensResponse = await _jwtService
            .GenerateAuthenticationResponseAsync(userToSignIn, cancellationToken);
        return Result.Ok(tokensResponse);
        
        //TODO: Refactor the UsersDomainService, possible extract some methods to different service
        //https://stackoverflow.com/questions/17745937/ddd-domain-services-what-should-a-service-class-contain
        //https://dev.to/ruben_alapont/domain-services-and-factories-in-domain-driven-design-55lo
    }
}