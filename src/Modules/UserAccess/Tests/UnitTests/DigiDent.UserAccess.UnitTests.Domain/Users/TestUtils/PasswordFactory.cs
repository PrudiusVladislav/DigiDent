
using DigiDent.UserAccess.Domain.Users.ValueObjects;

namespace DigiDent.Domain.UnitTests.UserAccess.Users.TestUtils;

public static class PasswordFactory
{
    public static HashSet<string> ValidPasswords =>
        [
            "correcthorsebatterystaple",
            "neverforget13/3/1997",
            "do you know",
            "ryanhunter2000"
        ];

    public static HashSet<string> InvalidPasswords =>
        [
            "",
            "qwerty",
            "password",
            "Tr0ub4dour&3",
            "abcdefghijk987654321",
            "12345678"
        ];

    public static IEnumerable<(string, string, bool)> PasswordsEqualityExpectations => 
        [
            (ValidPasswords.First(), ValidPasswords.First(), true),
            (ValidPasswords.First(), ValidPasswords.Last(), false)
        ];
    
    public static Password GetValidPassword()
    {
        return Password.Create(ValidPasswords.First()).Value!;
    }
}