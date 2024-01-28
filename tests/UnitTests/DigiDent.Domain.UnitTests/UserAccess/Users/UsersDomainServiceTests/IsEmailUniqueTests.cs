using DigiDent.Domain.UnitTests.Shared.ValueObjectsFactories;
using DigiDent.Domain.UnitTests.UserAccess.Users.TestUtils;
using DigiDent.Domain.UserAccessContext.Users;
using FluentAssertions;
using NSubstitute;

namespace DigiDent.Domain.UnitTests.UserAccess.Users.UsersDomainServiceTests;

public class IsEmailUniqueTests
{
    private readonly IUsersRepository _mockUsersRepository;
    private readonly UsersDomainService _usersDomainService;
    
    public IsEmailUniqueTests()
    {
        var mockUnitOfWork = Substitute.For<IUsersUnitOfWork>();
        _mockUsersRepository = Substitute.For<IUsersRepository>();
        mockUnitOfWork.UsersRepository.Returns(_mockUsersRepository);
        
        _usersDomainService = new UsersDomainService(mockUnitOfWork);
    }
    
    [Fact]
    public async Task IsEmailUnique_WhenEmailIsUnique_ReturnsTrue()
    {
        // Arrange
        var validUniqueEmail = EmailFactory.GetValidEmail();
        
        _mockUsersRepository
            .GetByEmailAsync(
                validUniqueEmail, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<User?>(null));
        
        // Act
        bool isEmailUnique = await _usersDomainService.IsEmailUniqueAsync(
            validUniqueEmail,
            CancellationToken.None);
        
        // Assert
        isEmailUnique.Should().BeTrue();
    }
    
    [Fact]
    public async Task IsEmailUnique_WhenEmailIsNotUnique_ReturnsFalse()
    {
        // Arrange
        var validUniqueEmail = EmailFactory.GetValidEmail();
        var returnedUser = UsersFactory.GetValidUser(email: validUniqueEmail);
        
        _mockUsersRepository
            .GetByEmailAsync(
                validUniqueEmail, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<User?>(returnedUser));
        
        // Act
        bool isEmailUnique = await _usersDomainService.IsEmailUniqueAsync(
            validUniqueEmail,
            CancellationToken.None);
        
        // Assert
        isEmailUnique.Should().BeFalse();
    }
}