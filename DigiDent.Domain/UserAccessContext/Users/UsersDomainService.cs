using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Users.Errors;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Domain.UserAccessContext.Users;

public class UsersDomainService
{
    private readonly IUsersRepository _usersRepository;
    
    public UsersDomainService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public async Task<Result<Email>> CreateEmailAsync(string value, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(value);
        if (emailResult.IsFailure)
            return emailResult;
        
        var user = await _usersRepository.GetByEmailAsync(emailResult.Value!, cancellationToken);
        if (user != null)
            return emailResult.AddError(EmailErrors.EmailIsNotUnique(emailResult.Value!.Value));
        
        return Result.Ok(emailResult.Value!);
    }
    
    public async Task<Result> MatchPasswordByEmailAsync(Email email, string passwordToCheck, CancellationToken cancellationToken)
    {
        var userToCheck = await _usersRepository.GetByEmailAsync(email, cancellationToken);
        if (userToCheck == null)
            return Result.Fail(EmailErrors.EmailIsNotRegistered(email.Value));
                
        return userToCheck.Password.IsEqualTo(passwordToCheck)
            ? Result.Ok()
            : Result.Fail(PasswordErrors.PasswordDoesNotMatch);
    }
    
}