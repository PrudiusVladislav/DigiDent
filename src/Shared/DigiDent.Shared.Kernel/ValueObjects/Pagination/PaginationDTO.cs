namespace DigiDent.Shared.Kernel.ValueObjects.Pagination;

public record PaginationDTO(
    int PageNumber = 1,
    int PageSize = 20,
    string SearchTerm = "",
    string SortByColumn = "Id",
    SortOrder SortOrder = SortOrder.Desc);
