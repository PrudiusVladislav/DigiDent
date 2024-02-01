using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.Shared.Kernel.Errors;

public static class PhoneNumberErrors
{
    public static Error InvalidPhoneNumber
        => new(
            ErrorType.Validation,
            nameof(PhoneNumber),
            "Invalid phone number. Allowed format: +380XXXXXXXXX. The +38 prefix is optional.");
}