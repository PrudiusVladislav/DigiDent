using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using FluentAssertions;

namespace DigiDent.Domain.UnitTests.UserAccess.Users.PasswordTests;

public class CreatePassword
{
    [Theory]
    [MemberData(nameof(CreateValidPasswords))]
    public void Create_WithValidPassword_ShouldCreateAndReturnPassword(string validPassword)
    {
        // Act
        Result<Password> passwordResult = Password.Create(validPassword);

        // Assert
        passwordResult.IsSuccess.Should().BeTrue();
        passwordResult.Value.Should().NotBeNull();
        passwordResult.Value?.PasswordHash.Should().NotBeNullOrWhiteSpace();
    }
    
    [Theory]
    [MemberData(nameof(CreateInvalidPasswords))]
    public void Create_WithInvalidPassword_ShouldReturnFailure(string invalidPassword)
    {
        // Act
        Result<Password> passwordResult = Password.Create(invalidPassword);

        // Assert
        passwordResult.IsFailure.Should().BeTrue();
        passwordResult.Value.Should().BeNull();
    }
    
    public static IEnumerable<object[]> CreateValidPasswords()
    {
        return PasswordFactory.ValidPasswords
            .Select(validPassword => (object[])[validPassword]);
    }
    
    public static IEnumerable<object[]> CreateInvalidPasswords()
    {
        return PasswordFactory.InvalidPasswords
            .Select(invalidPassword => (object[])[invalidPassword]);
    }
}