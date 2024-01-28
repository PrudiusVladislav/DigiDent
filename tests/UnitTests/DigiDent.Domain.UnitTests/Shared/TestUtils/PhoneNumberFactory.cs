using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.UnitTests.Shared.TestUtils;

public class PhoneNumberFactory
{
    public static PhoneNumber GetValidPhoneNumber()
    {
        return PhoneNumber.Create("+380123456789").Value!;
    }
}