using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.Shared.Infrastructure.Time;

public class DateTimeProvider: IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}