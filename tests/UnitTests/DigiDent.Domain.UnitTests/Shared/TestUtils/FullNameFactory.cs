using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.UnitTests.Shared.TestUtils;

public class FullNameFactory
{
    public static FullName GetValidFullName()
    {
        return FullName.Create("John", "Doe").Value!;
    }
}