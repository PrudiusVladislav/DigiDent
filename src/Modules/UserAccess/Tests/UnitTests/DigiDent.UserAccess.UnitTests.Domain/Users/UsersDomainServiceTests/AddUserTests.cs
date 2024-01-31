using DigiDent.Shared.Kernel.ValueObjects;
using DigiDent.UserAccess.Domain.Users;
using DigiDent.UserAccess.Domain.Users.Abstractions;
using DigiDent.UserAccess.Domain.Users.Services;
using DigiDent.UserAccess.Domain.Users.ValueObjects;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using UsersFactory = DigiDent.UserAccess.UnitTests.Domain.Users.TestUtils.UsersFactory;

namespace DigiDent.UserAccess.UnitTests.Domain.Users.UsersDomainServiceTests;

public class AddUserTests
{
    private readonly IUsersRepository _mockUsersRepository;
    private readonly IUsersUnitOfWork _mockUnitOfWork;  
    private readonly UsersDomainService _usersDomainService;
    
    public AddUserTests()
    {
        _mockUsersRepository = Substitute.For<IUsersRepository>();
        _mockUnitOfWork = Substitute.For<IUsersUnitOfWork>();
        _mockUnitOfWork.UsersRepository.Returns(_mockUsersRepository);
        _usersDomainService = new UsersDomainService(_mockUnitOfWork);
    }
    
    [Fact]
    public async Task AddUser_WithNonAdminRole_ShouldAddUser()
    {
        // Arrange
        var userToAdd = UsersFactory.GetValidUser(role: Role.Doctor);

        // Act
        await _usersDomainService.AddUserAsync(userToAdd, CancellationToken.None);

        // Assert
        
        await _mockUsersRepository
            .Received(1)
            .AddAsync(userToAdd, Arg.Any<CancellationToken>());
        
        await _mockUnitOfWork
            .Received(1)
            .CommitAsync(Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task AddUserAsync_WithAdminRoleAndExistingTempAdmin_ShouldDeleteTempAdminAndAddUser()
    {
        // Arrange
        var userToAdd = UsersFactory.GetValidUser(role: Role.Administrator);
        var tempAdmin = UsersFactory.GetValidUser(
            email: Email.TempAdminEmail,
            role: Role.Administrator);
        
        _mockUsersRepository
            .GetByEmailAsync(
                Email.TempAdminEmail, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<User?>(tempAdmin));

        // Act
        await _usersDomainService.AddUserAsync(userToAdd, CancellationToken.None);

        // Assert
        await _mockUsersRepository
            .Received(1)
            .DeleteAsync(Arg.Is(tempAdmin.Id), Arg.Any<CancellationToken>());
        
        await _mockUsersRepository
            .Received(1)
            .AddAsync(Arg.Is(userToAdd), Arg.Any<CancellationToken>());
        
        await _mockUnitOfWork
            .Received(1)
            .CommitAsync(Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task AddUserAsync_WithAdminRoleAndNoTempAdmin_ShouldAddUser()
    {
        // Arrange
        var userToAdd = UsersFactory.GetValidUser(role: Role.Administrator);
        
        _mockUsersRepository
            .GetByEmailAsync(
                Email.TempAdminEmail, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<User?>(null));

        // Act
        await _usersDomainService.AddUserAsync(userToAdd, CancellationToken.None);

        // Assert
        await _mockUsersRepository
            .DidNotReceive()
            .DeleteAsync(Arg.Any<UserId>(), Arg.Any<CancellationToken>());
        
        await _mockUsersRepository
            .Received(1)
            .AddAsync(Arg.Is(userToAdd), Arg.Any<CancellationToken>());
        
        await _mockUnitOfWork
            .Received(1)
            .CommitAsync(Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task AddUser_WithException_ShouldRollbackTransaction()
    {
        // Arrange
        var userToAdd = UsersFactory.GetValidUser();
        
        _mockUsersRepository
            .AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>())
            .Throws(new Exception("Simulated exception"));

        // Act
        var exception = await Record.ExceptionAsync(
             () => _usersDomainService.AddUserAsync(userToAdd, CancellationToken.None));

        // Assert
        exception.Should().NotBeNull();
        
        await _mockUnitOfWork
            .Received(1)
            .RollbackAsync(Arg.Any<CancellationToken>());
    }
}