using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.UnitTests.Shared.ValueObjectsFactories;

public class PhoneNumberFactory
{
    public static PhoneNumber GetValidPhoneNumber()
    {
        return PhoneNumber.Create("+380123456789").Value!;
    }
}