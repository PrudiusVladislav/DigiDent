using DigiDent.Shared.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Email = DigiDent.Shared.Kernel.ValueObjects.Email;
using PhoneNumber = DigiDent.Shared.Kernel.ValueObjects.PhoneNumber;

namespace DigiDent.Shared.Infrastructure.Persistence.EfCore.Converters;

/// <summary>
/// Contains EF Core value objects converters for shared value objects.
/// </summary>
public static class ValueObjectsConverters
{
    public static ValueConverter<Email, string> EmailConverter =>
        new ValueConverter<Email, string>(
            email => email.Value,
            value => new Email(value));

    public static ValueConverter<PhoneNumber, string> PhoneNumberConverter =>
        new ValueConverter<PhoneNumber, string>(
            phoneNumber => phoneNumber.Value,
            value => new PhoneNumber(value));

    public static ValueConverter<FullName, string> FullNameConverter =>
        new ValueConverter<FullName, string>(
            fullName => fullName.ToString(),
            value => new FullName(value));
}