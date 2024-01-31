using DigiDent.ClinicManagement.Domain.Visits.Errors;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Visits.ValueObjects;

public record VisitDateTime
{
    public DateTime Value { get; }

    internal VisitDateTime(DateTime value)
    {
        Value = value;
    }
    
    public static Result<VisitDateTime> Create(
        DateTime value, IDateTimeProvider dateTimeProvider)
    {
        if (value < dateTimeProvider.Now)
        {
            return Result.Fail<VisitDateTime>(VisitDateTimeErrors
                .VisitDateTimeIsInThePast);
        }

        return Result.Ok(new VisitDateTime(value));
    }
}