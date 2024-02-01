using DigiDent.Shared.Kernel.ValueObjects;
using DigiDent.Shared.UnitTests.Domain.Extensions;
using DigiDent.Shared.UnitTests.Domain.TestUtils;
using DigiDent.UserAccess.Domain.Users;
using DigiDent.UserAccess.Domain.Users.Events;
using DigiDent.UserAccess.UnitTests.Domain.Users.Extensions;
using DigiDent.UserAccess.UnitTests.Domain.Users.TestUtils;

namespace DigiDent.UserAccess.UnitTests.Domain.Users;

public class CreateUserTests
{
    [Fact]
    public void Create_WithValidData_ShouldReturnCreatedAndRaiseEvent()
    {
        // Arrange
        var fullName = FullNameFactory.GetValidFullName();
        var email = EmailFactory.GetValidEmail();
        var phoneNumber = PhoneNumberFactory.GetValidPhoneNumber();
        var password = PasswordFactory.GetValidPassword();
        var role = Role.Administrator;
        
        // Act
        User user = new(fullName, email, phoneNumber, password, role);
        
        // Assert
        user.ShouldBeCreatedFrom(fullName, email, phoneNumber, password, role);
        user.ShouldRaiseDomainEvent<UserSignedUpDomainEvent>();
    }
}