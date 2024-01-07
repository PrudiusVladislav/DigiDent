using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Application.UserAccess.Commands.SignIn;
using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using Mediator;

namespace DigiDent.Application.UserAccess.Commands.SignUp;

public class SignUpCommandHandler
    : IRequestHandler<SignUpCommand, Result<SignUpResponse>> 
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUsersRepository _usersRepository;
    private readonly UsersDomainService _usersDomainService;
    
    public SignUpCommandHandler(
        IJwtProvider jwtProvider,
        IUsersRepository usersRepository,
        UsersDomainService usersDomainService)
    {
        _jwtProvider = jwtProvider;
        _usersRepository = usersRepository;
        _usersDomainService = usersDomainService;
    }
    
    public async ValueTask<Result<SignUpResponse>> Handle(
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
            return validationResult.MapToType<SignUpResponse>();
        
        var userToAdd = User.Create(
            TypedId<Guid>.NewGuid<UserId>(),
            fullNameResult.Value!,
            emailResult.Value!,
            passwordResult.Value!,
            roleResult.Value!);
        await _usersRepository.AddAsync(userToAdd, cancellationToken);
        
        var jwt = _jwtProvider.GenerateJwtToken(userToAdd);
        return Result.Ok(new SignUpResponse(jwt));
    }
}