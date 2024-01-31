using DigiDent.Shared.Domain.ValueObjects;
using DigiDent.UserAccess.Domain.Users.Abstractions;

namespace DigiDent.UserAccess.Domain.Users.Services;

public class UsersDomainService
{
    private readonly IUsersUnitOfWork _unitOfWork;
    
    public UsersDomainService(IUsersUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> IsEmailUniqueAsync(
        Email value, CancellationToken cancellationToken)
    {
        User? user = await _unitOfWork.UsersRepository.GetByEmailAsync(
            value, cancellationToken);
        return user is null;
    }
    
    public async Task AddUserAsync(User userToAdd, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            if (userToAdd.Role != Role.Administrator)
            {
                await _unitOfWork.UsersRepository.AddAsync(userToAdd, cancellationToken);
            }
            else
            {
                User? tempAdmin = await _unitOfWork.UsersRepository
                    .GetByEmailAsync(UsersFactory.TempAdminEmail, cancellationToken);

                if (tempAdmin is not null)
                {
                    await _unitOfWork.UsersRepository
                        .DeleteAsync(tempAdmin.Id, cancellationToken);
                }

                await _unitOfWork.UsersRepository.AddAsync(userToAdd, cancellationToken);
            }

            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}