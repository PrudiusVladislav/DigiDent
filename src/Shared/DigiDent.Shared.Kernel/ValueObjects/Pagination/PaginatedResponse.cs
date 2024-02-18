namespace DigiDent.Shared.Kernel.ValueObjects.Pagination;

public record PaginatedResponse<T>(
    IReadOnlyCollection<T> DataCollection,
    int TotalCount);
