using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Visits.Errors;

public static class ProvidedServiceErrors
{
    public static Error NameHasInvalidLength(int minLength, int maxLength)
        => new (
            ErrorType.Validation,
            nameof(ProvidedService),
            $"Name must be between {minLength} and {maxLength} characters long.");
    
    public static Error DescriptionHasInvalidLength(int maxLength)
        => new (
            ErrorType.Validation,
            nameof(ProvidedService),
            $"Description must be non-empty and less than {maxLength} characters long.");
}