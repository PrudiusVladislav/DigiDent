namespace DigiDent.Shared.Domain.Abstractions;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}