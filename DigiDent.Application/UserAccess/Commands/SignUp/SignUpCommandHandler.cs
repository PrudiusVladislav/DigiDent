using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.SignUp;

public class SignUpCommandHandler
    : IRequestHandler<SignUpCommand, Result<SignUpResponse>> 
{
    private readonly IUsersRepository _usersRepository;
    private readonly UsersDomainService _usersDomainService;
    
    public SignUpCommandHandler(
        IUsersRepository usersRepository,
        UsersDomainService usersDomainService)
    {
        _usersRepository = usersRepository;
        _usersDomainService = usersDomainService;
    }
    
    public async Task<Result<SignUpResponse>> Handle(
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
        
        return Result.Ok(new SignUpResponse(userToAdd.Id.Value));
    }
}