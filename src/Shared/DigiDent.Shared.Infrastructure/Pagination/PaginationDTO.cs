using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.Shared.Infrastructure.Pagination;

public record PaginationDTO(
    int PageNumber = 1,
    int PageSize = 20,
    string SearchTerm = "",
    string SortByColumn = "",
    SortOrder SortOrder = SortOrder.Desc): IPaginationData;