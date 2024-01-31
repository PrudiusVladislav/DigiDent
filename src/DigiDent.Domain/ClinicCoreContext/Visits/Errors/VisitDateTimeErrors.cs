using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Errors;

public static class VisitDateTimeErrors
{
    public static Error VisitDateTimeIsInThePast
        => new(
            ErrorType.Validation,
            nameof(VisitDateTime), 
            "Visit date time cannot be in the past.");
}