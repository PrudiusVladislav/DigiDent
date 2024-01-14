using DigiDent.Domain.ClinicCoreContext.Visits.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;

public class TreatmentPlanDetails
{
    public string Diagnosis { get; private set; }
    public string? PlanDescription { get; private set; }
    
    internal TreatmentPlanDetails(string diagnosis, string? planDescription)
    {
        Diagnosis = diagnosis;
        PlanDescription = planDescription;
    }
    
    public static Result<TreatmentPlanDetails> Create(string diagnosis, string? planDescription)
    {
        if (string.IsNullOrWhiteSpace(diagnosis) || 
            string.IsNullOrWhiteSpace(planDescription))
        {
            return Result.Fail<TreatmentPlanDetails>(TreatmentPlanErrors
                .TreatmentPlanDetailsAreInvalid);
        }
        
        return Result.Ok(new TreatmentPlanDetails(
            diagnosis, planDescription));
    }
}