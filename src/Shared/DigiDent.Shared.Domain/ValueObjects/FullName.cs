using DigiDent.Shared.Domain.Errors;
using DigiDent.Shared.Domain.ReturnTypes;

namespace DigiDent.Shared.Domain.ValueObjects;

public record FullName
{
    public string FirstName { get; }
    public string LastName { get; }

    internal FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    
    // For EF Core
    internal FullName(string fullName)
    {
        var names = fullName.Split(' ');
        FirstName = names[0];
        LastName = names[1];
    }
    
    public static Result<FullName> Create(string firstName, string lastName)
    {
        Result firstNameResult = ValidateName(nameof(FirstName), firstName);
        Result lastNameResult = ValidateName(nameof(LastName), lastName);
        
        Result validationResult = Result.Merge(firstNameResult, lastNameResult);
        
        return validationResult.Match(
            onFailure: _ => validationResult.MapToType<FullName>(),
            onSuccess: () => Result.Ok(new FullName(firstName, lastName)));
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }

    private static Result ValidateName(string propertyName, string nameValue)
    {
        const int minLength = 2;
        const int maxLength = 50;
        
        if (string.IsNullOrWhiteSpace(nameValue))
            return Result.Fail(FullNameErrors.NameIsEmpty(propertyName));

        if (nameValue.Length is < minLength or > maxLength)
            return Result.Fail(FullNameErrors
                .NameHasInvalidLength(propertyName, minLength, maxLength));

        return Result.Ok();
    }

}