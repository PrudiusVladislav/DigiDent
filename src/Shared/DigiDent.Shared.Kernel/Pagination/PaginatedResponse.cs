namespace DigiDent.Shared.Kernel.Pagination;

public record PaginatedResponse<T>(
    IReadOnlyCollection<T> DataCollection,
    int TotalCount);
