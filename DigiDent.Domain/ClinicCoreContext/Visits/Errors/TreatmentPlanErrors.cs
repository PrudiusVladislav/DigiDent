using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Errors;

public static class TreatmentPlanErrors
{
    public static Error TreatmentPlanDetailsAreInvalid 
        => new(
            ErrorType.Validation,
            nameof(TreatmentPlanDetails),
            "Treatment plan details are invalid");
}