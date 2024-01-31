namespace DigiDent.Shared.Kernel.Abstractions;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}