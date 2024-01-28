using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.UnitTests.Shared.TestUtils;

public class FullNameFactory
{
    public static HashSet<(string, string)> ValidNames =>
    [
        ("John", "Doe"),
        ("Emma", "Johnson"),
        ("Christopher", "Smith"),
        ("Olivia", "Williams"),
        ("William", "Brown"),
        ("Ava", "Jones")
    ];

    public static HashSet<(string, string)> InvalidNames() =>
    [
        ("John", ""),
        ("", "Doe"),
        ("J", "Doe"),
        ("John", string.Join("", Enumerable.Range(0, 17).Select(x => "Doe"))) //length == 51
    ];
    
    public static FullName GetValidFullName()
    {
        return FullName.Create("John", "Doe").Value!;
    }
}