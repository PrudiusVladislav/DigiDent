using DigiDent.ClinicManagement.Domain.Shared.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Shared.ValueObjects;

public record TimeDuration
{
    public TimeSpan Duration { get; }
    
    internal TimeDuration(TimeSpan duration)
    {
        Duration = duration;
    }
    
    public static Result<TimeDuration> Create(TimeSpan duration)
    {
        if (duration <= TimeSpan.Zero)
        {
            return Result.Fail<TimeDuration>(TimeErrors
                .DurationIsNotPositive);
        }
        
        return Result.Ok(new TimeDuration(duration));
    }
}