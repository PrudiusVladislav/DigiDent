using System.Text.RegularExpressions;
using DigiDent.Shared.Domain.Errors;
using DigiDent.Shared.Domain.ReturnTypes;

namespace DigiDent.Shared.Domain.ValueObjects;

public partial record Email
{
    public string Value { get;}
    
    internal Email(string value) => Value = value;
    
    public static Result<Email> Create(string value)
    {
        if (!EmailRegex().IsMatch(value))
            return Result.Fail<Email>(EmailErrors.EmailDoesNotMatchRules);
        
        return Result.Ok(new Email(value));
    }

    [GeneratedRegex(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$")]
    private static partial Regex EmailRegex();
}