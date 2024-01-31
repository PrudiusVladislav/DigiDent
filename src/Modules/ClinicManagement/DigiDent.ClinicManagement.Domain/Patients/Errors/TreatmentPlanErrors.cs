using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Shared.Domain.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Patients.Errors;

public static class TreatmentPlanErrors
{
    public static Error TreatmentPlanDetailsAreInvalid 
        => new(
            ErrorType.Validation,
            nameof(TreatmentPlanDetails),
            "Treatment plan details are invalid");
}