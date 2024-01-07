using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.Errors;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using Mediator;

namespace DigiDent.Application.UserAccess.Commands.SignIn;

public class SignInCommandHandler
    : IRequestHandler<SignInCommand, Result<SignInResponse>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly UsersDomainService _usersDomainService;
    private readonly IUsersRepository _usersRepository;
    
    public SignInCommandHandler(
        IJwtProvider jwtProvider,
        UsersDomainService usersDomainService,
        IUsersRepository usersRepository)
    {
        _jwtProvider = jwtProvider;
        _usersDomainService = usersDomainService;
        _usersRepository = usersRepository;
    }
    
    public async ValueTask<Result<SignInResponse>> Handle(
        SignInCommand request,
        CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
            return Result.Fail<SignInResponse>(EmailErrors
                .EmailIsNotRegistered(request.Email));
        
        var userToSignIn = await _usersRepository
            .GetByEmailAsync(emailResult.Value!, cancellationToken);
        if (userToSignIn == null)
            return Result.Fail<SignInResponse>(EmailErrors
                .EmailIsNotRegistered(request.Email));
        
        var roleResult = _usersDomainService.CreateRole(request.Role);
        if (roleResult.IsFailure || userToSignIn.Role != roleResult.Value)
            return Result.Fail<SignInResponse>(RoleErrors
                .RoleIsNotValid(request.Role));
        
        var isPasswordCorrect = userToSignIn.Password.IsEqualTo(request.Password);
        if (!isPasswordCorrect)
            return Result.Fail<SignInResponse>(PasswordErrors
                .PasswordDoesNotMatch);
        
        var jwt = _jwtProvider.GenerateJwtToken(userToSignIn);
        return Result.Ok(new SignInResponse(jwt));
        
        //TODO: Refactor the UsersDomainService, possible extract some methods to different service
        //https://stackoverflow.com/questions/17745937/ddd-domain-services-what-should-a-service-class-contain
        //https://dev.to/ruben_alapont/domain-services-and-factories-in-domain-driven-design-55lo
    }
}