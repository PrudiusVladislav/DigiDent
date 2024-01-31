using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Patients.Errors;

public static class TreatmentPlanErrors
{
    public static Error TreatmentPlanDetailsAreInvalid 
        => new(
            ErrorType.Validation,
            nameof(TreatmentPlanDetails),
            "Treatment plan details are invalid");
}