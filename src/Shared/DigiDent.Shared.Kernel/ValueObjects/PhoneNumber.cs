using System.Text.RegularExpressions;
using DigiDent.Shared.Kernel.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.Shared.Kernel.ValueObjects;

/// <summary>
/// Represents a phone number (supports only Ukrainian phone numbers).
/// </summary>
public partial record PhoneNumber
{
    public string Value { get; init; }
    
    internal PhoneNumber(string value)
    {
        Value = value;
    }
    
    public static Result<PhoneNumber> Create(string value)
    {
        if (!PhoneNumberRegex().IsMatch(value))
        {
            return Result.Fail<PhoneNumber>(PhoneNumberErrors
                .InvalidPhoneNumber);
        }
        
        return Result.Ok(new PhoneNumber(value));
    }

    [GeneratedRegex(@"^(?:\+38)?(0\d{9})$")]
    private static partial Regex PhoneNumberRegex();
}