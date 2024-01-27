using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Shared.Errors;

public static class TimeErrors
{
    public static Error DurationIsNotPositive
        => new (
            ErrorType.Validation,
            nameof(TimeDuration),
            "The duration must be positive.");
}