using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Roles;
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
    
    public async Task<Result> MatchPasswordByEmailAsync(
        Email email,
        string passwordToCheck,
        CancellationToken cancellationToken)
    {
        var userToCheck = await _usersRepository.GetByEmailAsync(email, cancellationToken);
        if (userToCheck == null)
            return Result.Fail(EmailErrors.EmailIsNotRegistered(email.Value));
                
        return userToCheck.Password.IsEqualTo(passwordToCheck)
            ? Result.Ok()
            : Result.Fail(PasswordErrors.PasswordDoesNotMatch);
    }
    
    public async Task<Result<UserId>> AddUserAsync(
        string firstName, 
        string lastName,
        string email,
        string password,
        string role,
        CancellationToken cancellationToken)
    {
        var fullNameResult = FullName.Create(firstName, lastName);
        var emailResult = await CreateEmailAsync(email, cancellationToken);
        var passwordResult = Password.Create(password);
        var roleResult = Role.Create(role);
        var validationResult = Result.Merge<UserId>(fullNameResult, emailResult, passwordResult);
        
        return await validationResult.MatchAsync(
            onFailure: _ => validationResult,
            onSuccess: async () =>
            {
                var user = User.Create(
                    TypedId<Guid>.NewGuid<UserId>(),
                    fullNameResult.Value!,
                    emailResult.Value!,
                    passwordResult.Value!,
                    roleResult.Value!);
                await _usersRepository.AddAsync(user, cancellationToken);
                return Result.Ok(user.Id);
            });
    }

    public async Task<Result> UpdatePasswordByEmailAsync(
        Email email,
        string oldPassword,
        string newPassword,
        CancellationToken cancellationToken)
    {
        var checkOldPasswordResult = await MatchPasswordByEmailAsync(email, oldPassword, cancellationToken);
        var newPasswordResult = Password.Create(newPassword);
        
        var validationResult = Result.Merge<bool>(checkOldPasswordResult, newPasswordResult);
        if (validationResult.IsFailure)
            return validationResult;
        
        var user = await _usersRepository.GetByEmailAsync(email, cancellationToken);
                
        user!.Password.Update(newPasswordResult.Value!);
        await _usersRepository.UpdateAsync(user, cancellationToken);
        return Result.Ok();
    }
}