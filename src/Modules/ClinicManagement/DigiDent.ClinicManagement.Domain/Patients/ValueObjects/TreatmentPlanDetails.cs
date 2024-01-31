using DigiDent.ClinicManagement.Domain.Patients.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Patients.ValueObjects;

public class TreatmentPlanDetails
{
    public string DiagnosisDescription { get; private set; }
    
    internal TreatmentPlanDetails(string diagnosisDescription)
    {
        DiagnosisDescription = diagnosisDescription;
    }
    
    public static Result<TreatmentPlanDetails> Create(string diagnosisDescription)
    {
        const int diagnosisDescriptionMinLength = 3;
        
        if (string.IsNullOrWhiteSpace(diagnosisDescription) ||
            diagnosisDescription.Length < diagnosisDescriptionMinLength)
        {
            return Result.Fail<TreatmentPlanDetails>(TreatmentPlanErrors
                .TreatmentPlanDetailsAreInvalid);
        }
        
        return Result.Ok(new TreatmentPlanDetails(
            diagnosisDescription));
    }
}