using DigiDent.Shared.Domain.ValueObjects;
using DigiDent.Shared.UnitTests.Domain.TestUtils;
using DigiDent.UserAccess.Domain.Users;
using DigiDent.UserAccess.Domain.Users.ValueObjects;

namespace DigiDent.Domain.UnitTests.UserAccess.Users.TestUtils;

public class UsersFactory
{
    public static User GetValidUser(
        FullName? fullName = null,
        Email? email = null,    
        PhoneNumber? phoneNumber = null,
        Password? password = null,
        Role? role = null)
    {
        return User.Create(
            fullName ?? FullNameFactory.GetValidFullName(),
            email ?? EmailFactory.GetValidEmail(),
            phoneNumber ?? PhoneNumberFactory.GetValidPhoneNumber(),
            password ?? PasswordFactory.GetValidPassword(),
            role ?? Role.Administrator);
    }
}