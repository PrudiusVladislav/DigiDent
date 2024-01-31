namespace DigiDent.Domain.SharedKernel.Abstractions;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}