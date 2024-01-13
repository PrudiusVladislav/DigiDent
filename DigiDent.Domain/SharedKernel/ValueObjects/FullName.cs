using DigiDent.Domain.SharedKernel.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.SharedKernel.ValueObjects;

public record FullName
{
    public string FirstName { get; }
    public string LastName { get; }
    
    private const int MinLength = 2;
    private const int MaxLength = 50;

    internal FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    
    public static Result<FullName> Create(string firstName, string lastName)
    {
        var firstNameValidationResult = ValidateFirstName(firstName);
        var lastNameValidationResult = ValidateLastName(lastName);
        
        var validationResult = Result.Merge(firstNameValidationResult, lastNameValidationResult);
        
        return validationResult.Match(
            onFailure: _ => validationResult.MapToType<FullName>(),
            onSuccess: () => Result.Ok(new FullName(firstName, lastName)));
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }

    //for ef core
    internal static FullName CreateFromNameParts(string[] nameParts)
    {
        return new FullName(nameParts[0], nameParts[1]);
    }

    private static Result ValidateFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Fail(FullNameErrors.FirstNameIsEmpty);
        
        if (firstName.Length is < MinLength or > MaxLength)
            return Result.Fail(FullNameErrors.FirstNameHasInvalidLength(MinLength, MaxLength));
        
        return Result.Ok();
    }

    private static Result ValidateLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Fail(FullNameErrors.LastNameIsEmpty);
        
        if (lastName.Length is < MinLength or > MaxLength)
            return Result.Fail(FullNameErrors.LastNameHasInvalidLength(MinLength, MaxLength));
        
        return Result.Ok();
    }
    
}