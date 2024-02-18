namespace DigiDent.Shared.Abstractions.Queries.Pagination;

public record PaginationDTO(
    int PageNumber,
    int PageSize = 20,
    string SearchTerm = "",
    string SortByColumn = "Id",
    SortOrder SortOrder = SortOrder.Descending);
