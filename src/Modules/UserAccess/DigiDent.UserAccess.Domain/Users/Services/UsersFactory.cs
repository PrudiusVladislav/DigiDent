using DigiDent.Shared.Kernel.ValueObjects;
using DigiDent.UserAccess.Domain.Users.ValueObjects;
using Email = DigiDent.Shared.Kernel.ValueObjects.Email;
using PhoneNumber = DigiDent.Shared.Kernel.ValueObjects.PhoneNumber;

namespace DigiDent.UserAccess.Domain.Users.Services;

public static class UsersFactory
{
    internal static User CreateTempUserAdmin()
    {
        return new User(
            TempAdminFullName,
            TempAdminEmail, 
            TempAdminPhoneNumber, 
            TempAdminPassword, 
            Role.Administrator);
    }
    
    internal static Email TempAdminEmail
        => new("temp@admin.tmp");
    
    internal static PhoneNumber TempAdminPhoneNumber
        => new("+380000000000");
    
    internal static FullName TempAdminFullName
        => new("Temporary", "Administrator");
    
    private static Password TempAdminPassword 
        => Password.Create("*tempAdminPass!").Value!;
}