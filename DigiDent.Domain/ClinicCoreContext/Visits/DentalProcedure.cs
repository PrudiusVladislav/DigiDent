using DigiDent.Domain.ClinicCoreContext.Visits.Errors;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits;

public class DentalProcedure: IEntity<DentalProcedureId, int>
{
    private const int NameMinLength = 3;
    private const int NameMaxLength = 100;
    
    private const int DescriptionMaxLength = 500;
    
    public DentalProcedureId Id { get; init; }
    
    public string Name { get; init; }
    public string? Description { get; init; }
    
    public TimeSpan UsualDuration { get; init; }
    
    public Money Price { get; init; }
    
    internal DentalProcedure(
        string name,
        string? description,
        TimeSpan usualDuration,
        Money price)
    {
        Name = name;
        Description = description;
        UsualDuration = usualDuration;
        Price = price;
    }
    
    public static Result<DentalProcedure> Create(
        string name,
        string? description,
        TimeSpan usualDuration,
        Money price)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            name.Length < NameMinLength ||
            name.Length > NameMaxLength)
        {
            return Result.Fail<DentalProcedure>(DentalProcedureErrors
                .NameHasInvalidLength(NameMinLength, NameMaxLength));
        }
        
        if (description?.Length > DescriptionMaxLength)
        {
            return Result.Fail<DentalProcedure>(DentalProcedureErrors
                .DescriptionHasInvalidLength(DescriptionMaxLength));
        }
        
        if (usualDuration <= TimeSpan.Zero)
        {
            return Result.Fail<DentalProcedure>(DentalProcedureErrors
                .UsualDurationIsNotPositive);
        }
        
        return Result.Ok(new DentalProcedure(
            name,
            description,
            usualDuration,
            price));
    }
}