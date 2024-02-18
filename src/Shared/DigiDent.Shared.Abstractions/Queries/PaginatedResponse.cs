namespace DigiDent.Shared.Abstractions.Queries;

public record PaginatedResponse<T>(
    IReadOnlyCollection<T> DataCollection,
    int TotalCount);
