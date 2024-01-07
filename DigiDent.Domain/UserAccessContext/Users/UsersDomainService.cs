using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Users.DTO;
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
            return emailResult.AddError(EmailErrors
                .EmailIsNotUnique(emailResult.Value!.Value));
        
        return Result.Ok(emailResult.Value!);
    }

    public Result<Role> CreateRole(string roleName)
    {
        var isRoleValid = Enum.TryParse<Role>(roleName, true, out var role);
        if (!isRoleValid)
            return Result.Fail<Role>(RoleErrors.RoleIsNotValid(roleName));
        return Result.Ok(role);
    }
    
    // public async Task<Result> MatchPasswordByEmailAsync(
    //     Email email,
    //     string passwordToCheck,
    //     CancellationToken cancellationToken)
    // {
    //     var userToCheck = await _usersRepository.GetByEmailAsync(email, cancellationToken);
    //     if (userToCheck == null)
    //         return Result.Fail(EmailErrors.EmailIsNotRegistered(email.Value));
    //             
    //     return userToCheck.Password.IsEqualTo(passwordToCheck)
    //         ? Result.Ok()
    //         : Result.Fail(PasswordErrors.PasswordDoesNotMatch);
    // }
    
    /*public async Task<Result<UserId>> AddUserAsync(
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
        var roleResult = CreateRole(role);
        
        var validationResult = Result.Merge(
            fullNameResult, emailResult, passwordResult);
        
        return await validationResult.MatchAsync(
            onFailure: _ => validationResult.MapToType<UserId>(),
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
    }*/
    
    /*public async Task<Result> UpdateUserByEmailAsync(
        Email email,
        string password,
        string? firstName = null,
        string? lastName = null,
        Role? role = null,
        CancellationToken cancellationToken = default)
    {
        var matchPasswordResult = await MatchPasswordByEmailAsync(email, password, cancellationToken);
        if (matchPasswordResult.IsFailure) return matchPasswordResult;
        
        User userToUpdate = (await _usersRepository.GetByEmailAsync(email, cancellationToken))!;

        var updateUserDto = UpdateUserDto.CopyFromUser(userToUpdate);
        
        var updateUserNameResult = ValidateAndUpdateUserName(
            userToUpdate,
            updateUserDto,
            firstName,
            lastName);
        
        var updateRoleResult = ValidateAndUpdateRole(updateUserDto, role);
        
        var updateResult = Result.Merge(updateUserNameResult, updateRoleResult);
        return await updateResult.MatchAsync(
            onFailure: _ => updateResult,
            onSuccess: async () =>
            {
                await _usersRepository.UpdateAsync(updateUserDto, cancellationToken);
                return Result.Ok();
            });
    }*/
    
    private Result ValidateAndUpdateUserName(
        User userToUpdate,
        UpdateUserDto updateUserDto,
        string? firstName,
        string? lastName)
    {
        if (firstName == null && lastName == null)
            return Result.Ok();
        
        var fullNameResult = FullName.Create(
            firstName ?? userToUpdate.FullName.FirstName, 
            lastName ?? userToUpdate.FullName.LastName);
            
        return fullNameResult.Match(
            onFailure: _ => fullNameResult,
            onSuccess: () =>
            {
                updateUserDto.FullName = fullNameResult.Value!;
                return Result.Ok();
            });
    }
    
    private Result ValidateAndUpdateRole(UpdateUserDto updateUserDto, Role? role)
    {
        if (role != null)
            updateUserDto.Role = role.Value;
        return Result.Ok();
    }

    //TODO: implement the change password method
    // public async Task<Result> UpdateUser(
    //     Email email,
    //     string oldPassword,
    //     string newPassword,
    //     CancellationToken cancellationToken)
    // {
    //     var checkOldPasswordResult = await MatchPasswordByEmailAsync(
    //         email, oldPassword, cancellationToken);
    //     var newPasswordResult = Password.Create(newPassword);
    //     
    //     var validationResult = Result.Merge<bool>(
    //         checkOldPasswordResult, newPasswordResult);
    //
    //     return await validationResult.MatchAsync(
    //         onFailure: _ => validationResult,
    //         onSuccess: async () =>
    //         {
    //             User user = (await _usersRepository.GetByEmailAsync(email, cancellationToken))!;
    //             
    //             await _usersRepository.UpdateAsync(
    //                 cancellationToken,
    //                 password: newPasswordResult.Value!);
    //
    //             return Result.Ok<bool>(true);
    //         });
    // }
}