using DigiDent.Domain.ClinicCoreContext.Visits.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;

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