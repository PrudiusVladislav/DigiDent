using DigiDent.Shared.Domain.ValueObjects;

namespace DigiDent.Shared.UnitTests.Domain.TestUtils;

public class EmailFactory
{
    public static HashSet<string> ValidEmails =>
    [
        "john.doe123@example.com",
        "alice_87@company.net",
        "user.email@domain.org",
        "test-email@provider.co",
        "sam+123@webmail.info"
    ];
    
    public static HashSet<string> InvalidEmails =>
    [
        "invalid.email@missingtld",
        "user@inva lid.com",
        "missing.at.symboldomain.com",
        "plainaddress",
        "#@%^%#$@#$@#.com",
        "@example.com",
        "Joe Smith <email@example.com>",
        "email.example.com",
        "email@example@example.com"
    ];
    
    public static Email GetValidEmail()
    {
        return Email.Create(ValidEmails.First()).Value!;
    }
}