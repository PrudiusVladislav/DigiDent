using DigiDent.Domain.ClinicCoreContext.Visits.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;

/// <summary>
/// Contains details about provided service, such as name and description.
/// </summary>
public record ProvidedServiceDetails
{
    public string Name { get; init; }
    public string Description { get; init; }
    
    internal ProvidedServiceDetails(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public static Result<ProvidedServiceDetails> Create(
        string name,
        string description)
    {
        const int nameMinLength = 3;
        const int nameMaxLength = 100;
        const int descriptionMaxLength = 500;
        
        if (string.IsNullOrWhiteSpace(name) ||
            name.Length < nameMinLength ||
            name.Length > nameMaxLength)
        {
            return Result.Fail<ProvidedServiceDetails>(ProvidedServiceErrors
                .NameHasInvalidLength(nameMinLength, nameMaxLength));
        }
        
        if (string.IsNullOrWhiteSpace(description) ||
            description.Length > descriptionMaxLength)
        {
            return Result.Fail<ProvidedServiceDetails>(ProvidedServiceErrors
                .DescriptionHasInvalidLength(descriptionMaxLength));
        }
        
        return Result.Ok(new ProvidedServiceDetails(
            name, description));
    }
}