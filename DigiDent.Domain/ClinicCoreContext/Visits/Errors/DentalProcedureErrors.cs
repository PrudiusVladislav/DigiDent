using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Errors;

public static class DentalProcedureErrors
{
    public static Error NameHasInvalidLength(int minLength, int maxLength)
        => new Error(
            ErrorType.Validation,
            nameof(DentalProcedure),
            $"Name must be between {minLength} and {maxLength} characters long.");
    
    public static Error DescriptionHasInvalidLength(int maxLength)
        => new Error(
            ErrorType.Validation,
            nameof(DentalProcedure),
            $"Description must be less than {maxLength} characters long.");
}