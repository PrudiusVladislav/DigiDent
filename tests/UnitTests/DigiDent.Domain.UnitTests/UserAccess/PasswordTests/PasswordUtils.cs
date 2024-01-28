
namespace DigiDent.Domain.UnitTests.UserAccess.PasswordTests;

public static class PasswordUtils
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
}