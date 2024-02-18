namespace DigiDent.Shared.Kernel.ValueObjects.Pagination;

public record PaginationDTO(
    int PageNumber,
    int PageSize = 20,
    string SearchTerm = "",
    string SortByColumn = "Id",
    SortOrder SortOrder = SortOrder.Descending);
