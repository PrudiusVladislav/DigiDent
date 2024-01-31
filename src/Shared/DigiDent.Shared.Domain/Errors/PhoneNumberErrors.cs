using DigiDent.Shared.Domain.ReturnTypes;
using DigiDent.Shared.Domain.ValueObjects;

namespace DigiDent.Shared.Domain.Errors;

public static class PhoneNumberErrors
{
    public static Error InvalidPhoneNumber
        => new(
            ErrorType.Validation,
            nameof(PhoneNumber),
            "Invalid phone number. Allowed format: +380XXXXXXXXX. The +38 prefix is optional.");
}