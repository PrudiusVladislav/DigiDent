using DigiDent.Domain.SharedKernel.ValueObjects;
using DigiDent.Domain.UnitTests.Shared.ValueObjectsFactories;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

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