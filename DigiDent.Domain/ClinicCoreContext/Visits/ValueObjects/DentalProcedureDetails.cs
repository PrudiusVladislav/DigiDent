using DigiDent.Domain.ClinicCoreContext.Visits.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;

public class DentalProcedureDetails
{
    private const int NameMinLength = 3;
    private const int NameMaxLength = 100;
    private const int DescriptionMaxLength = 500;
    
    public string Name { get; set; }
    public string? Description { get; set; }
    
    internal DentalProcedureDetails(string name, string? description)
    {
        Name = name;
        Description = description;
    }
    
    public static Result<DentalProcedureDetails> Create(
        string name,
        string? description)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            name.Length < NameMinLength ||
            name.Length > NameMaxLength)
        {
            return Result.Fail<DentalProcedureDetails>(DentalProcedureErrors
                .NameHasInvalidLength(NameMinLength, NameMaxLength));
        }
        
        if (description?.Length > DescriptionMaxLength)
        {
            return Result.Fail<DentalProcedureDetails>(DentalProcedureErrors
                .DescriptionHasInvalidLength(DescriptionMaxLength));
        }
        
        return Result.Ok(new DentalProcedureDetails(
            name, description));
    }
}