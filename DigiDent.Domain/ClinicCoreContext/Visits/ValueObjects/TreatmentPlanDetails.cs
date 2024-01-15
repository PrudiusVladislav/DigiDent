using DigiDent.Domain.ClinicCoreContext.Visits.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;

public class TreatmentPlanDetails
{
    private const int DiagnosisDescriptionMinLength = 3;
    public string DiagnosisDescription { get; private set; }
    
    internal TreatmentPlanDetails(string diagnosisDescription)
    {
        DiagnosisDescription = diagnosisDescription;
    }
    
    public static Result<TreatmentPlanDetails> Create(string diagnosisDescription)
    {
        if (string.IsNullOrWhiteSpace(diagnosisDescription) ||
            diagnosisDescription.Length < DiagnosisDescriptionMinLength)
        {
            return Result.Fail<TreatmentPlanDetails>(TreatmentPlanErrors
                .TreatmentPlanDetailsAreInvalid);
        }
        
        return Result.Ok(new TreatmentPlanDetails(
            diagnosisDescription));
    }
}