using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Shared.Domain.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Errors;

public static class MoneyErrors
{
    public static Error AmountIsNegative
        => new(
            ErrorType.Validation,
            nameof(Money),
            "Amount cannot be negative.");
}