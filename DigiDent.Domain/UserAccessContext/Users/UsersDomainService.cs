using DigiDent.Domain.SharedKernel.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;
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
    
    public async Task AddUserAsync(User userToAdd, CancellationToken cancellationToken)
    {
        if (userToAdd.Role != Role.Administrator)
        {
            await _usersRepository.AddAsync(userToAdd, cancellationToken);
            return;
        }
        
        var tempAdmin = await _usersRepository
            .GetByEmailAsync(Email.TempAdminEmail, cancellationToken);
        if (tempAdmin != null)
            await _usersRepository.DeleteAsync(tempAdmin.Id, cancellationToken);
        
        await _usersRepository.AddAsync(userToAdd, cancellationToken);
    }

    public async Task<Result> DeleteUserAsync(UserId userId, CancellationToken cancellationToken)
    {
        var users = await _usersRepository.GetAllAsync(cancellationToken);
        var userToDelete = await _usersRepository.GetByIdAsync(userId, cancellationToken);
        if (userToDelete is null) return Result.Ok();
        
        if (userToDelete.Role == Role.Administrator)
        {
            if (users.Count(u => u.Role == Role.Administrator) == 1)
                return Result.Fail(UserErrors.CannotDeleteLastAdmin);
        }
        await _usersRepository.DeleteAsync(userToDelete.Id, cancellationToken);
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