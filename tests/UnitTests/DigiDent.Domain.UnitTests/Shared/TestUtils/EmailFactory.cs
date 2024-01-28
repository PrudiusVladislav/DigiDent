using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.UnitTests.Shared.TestUtils;

public class EmailFactory
{
    public static Email GetValidEmail()
    {
        return Email.Create("testing@email.com").Value!;
    }
}