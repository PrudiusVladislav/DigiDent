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

    public async Task<bool> IsEmailUnique(
        Email value, CancellationToken cancellationToken)
    {
        User? user = await _usersRepository.GetByEmailAsync(
            value, cancellationToken);
        return user is null;
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
}