using DigiDent.Shared.Domain.Abstractions;

namespace DigiDent.Infrastructure.Shared;

public class DateTimeProvider: IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}