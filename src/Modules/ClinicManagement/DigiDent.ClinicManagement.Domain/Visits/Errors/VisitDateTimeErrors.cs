using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Visits.Errors;

public static class VisitDateTimeErrors
{
    public static Error VisitDateTimeIsInThePast
        => new(
            ErrorType.Validation,
            nameof(VisitDateTime), 
            "Visit date time cannot be in the past.");
}