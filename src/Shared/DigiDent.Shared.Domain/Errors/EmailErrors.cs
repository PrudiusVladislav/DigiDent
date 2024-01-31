using DigiDent.Shared.Domain.Abstractions;
using DigiDent.Shared.Domain.ReturnTypes;
using DigiDent.Shared.Domain.ValueObjects;

namespace DigiDent.Shared.Domain.Errors;

public static class EmailErrors
{
    public static Error EmailDoesNotMatchRules 
        => new (
            ErrorType.Validation, 
            nameof(Email),
            "Email should match the default set of email creation rules.");
    
    public static Error EmailIsNotUnique(string email)
        => new (
            ErrorType.Conflict,
            nameof(Email),
            $"User with email {email} already exists.");
    
    public static Error EmailIsNotRegistered(string email)
        => new (
            ErrorType.NotFound,
            nameof(Email),
            $"User with email {email} is not registered.");
}