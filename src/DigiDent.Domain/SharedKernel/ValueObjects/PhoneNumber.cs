using System.Text.RegularExpressions;
using DigiDent.Domain.SharedKernel.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.SharedKernel.ValueObjects;

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
    
    internal static PhoneNumber TempAdminPhoneNumber 
        => new("+380000000000");

    [GeneratedRegex(@"^(?:\+38)?(0\d{9})$")]
    private static partial Regex PhoneNumberRegex();
}