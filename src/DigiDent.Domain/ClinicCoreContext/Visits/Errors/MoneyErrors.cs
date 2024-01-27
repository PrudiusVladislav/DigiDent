using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Errors;

public static class MoneyErrors
{
    public static Error AmountIsNegative
        => new(
            ErrorType.Validation,
            nameof(Money),
            "Amount cannot be negative.");
}