namespace DigiDent.Shared.Kernel.Abstractions;

public interface IPaginatedResponse<out T>
{
    IReadOnlyCollection<T> DataCollection { get; }
    int TotalCount { get; }
}