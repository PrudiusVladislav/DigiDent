using DigiDent.Domain.ClinicCoreContext.Visits.Errors;
using DigiDent.Shared.Domain.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;

/// <summary>
/// Represents money value object. Specifies the amount of money in hryvnias.
/// </summary>
public record Money
{
    public static Money Zero => new(0);
    public decimal Amount { get; }
    
    internal Money(decimal amount)
    {
        Amount = amount;
    }

    public static Result<Money> Create(decimal amount)
    {
        if (amount < 0)
        {
            return Result.Fail<Money>(MoneyErrors
                .AmountIsNegative);
        }
        
        return Result.Ok(new Money(amount));
    }
}