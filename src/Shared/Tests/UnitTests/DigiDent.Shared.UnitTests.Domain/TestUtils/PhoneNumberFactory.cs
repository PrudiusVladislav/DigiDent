using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.Shared.UnitTests.Domain.TestUtils;

public class PhoneNumberFactory
{
    public static HashSet<string> ValidPhoneNumbers =>
    [
        "+380987654321",
        "0987654321",
        "+380501234567",
        "0501234567",
        "+380671112233"
    ];

    public static HashSet<string> InvalidPhoneNumbers =>
    [
        "+3801234",
        "01234567890",
        "+48-123456789",
        "090-123-4567",
        "1234567890",
    ];
    
    public static PhoneNumber GetValidPhoneNumber()
    {
        return PhoneNumber.Create(ValidPhoneNumbers.First()).Value!;
    }
}