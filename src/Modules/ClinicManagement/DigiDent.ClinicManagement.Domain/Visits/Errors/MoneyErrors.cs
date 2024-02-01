using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Visits.Errors;

public static class MoneyErrors
{
    public static Error AmountIsNegative
        => new(
            ErrorType.Validation,
            nameof(Money),
            "Amount cannot be negative.");
}