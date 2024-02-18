namespace DigiDent.Shared.Abstractions.Queries.Pagination;

public record PaginatedResponse<T>(
    IReadOnlyCollection<T> DataCollection,
    int TotalCount);
