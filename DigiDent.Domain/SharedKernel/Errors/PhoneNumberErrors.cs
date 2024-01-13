using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.SharedKernel.Errors;

public static class PhoneNumberErrors
{
    public static Error InvalidPhoneNumber
        => new(
            ErrorType.Validation,
            nameof(PhoneNumber),
            "Invalid phone number. Allowed format: +380XXXXXXXXX. The +38 prefix is optional.");
}