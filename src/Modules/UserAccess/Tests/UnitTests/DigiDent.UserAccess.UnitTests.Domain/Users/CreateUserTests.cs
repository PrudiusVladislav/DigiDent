using DigiDent.Shared.Domain.ValueObjects;
using DigiDent.Shared.UnitTests.Domain.Extensions;
using DigiDent.Shared.UnitTests.Domain.TestUtils;
using DigiDent.Domain.UnitTests.UserAccess.Users.Extensions;
using DigiDent.Domain.UnitTests.UserAccess.Users.TestUtils;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.Events;

namespace DigiDent.Domain.UnitTests.UserAccess.Users;

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
        var user = User.Create(fullName, email, phoneNumber, password, role);
        
        // Assert
        user.ShouldBeCreatedFrom(fullName, email, phoneNumber, password, role);
        user.ShouldRaiseDomainEvent<UserSignedUpDomainEvent>();
    }
}