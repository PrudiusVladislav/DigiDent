using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Shared.Errors;

public static class TimeErrors
{
    public static Error DurationIsNotPositive
        => new (
            ErrorType.Validation,
            nameof(TimeDuration),
            "The duration must be positive.");
}