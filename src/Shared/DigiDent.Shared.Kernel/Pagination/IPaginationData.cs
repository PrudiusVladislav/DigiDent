namespace DigiDent.Shared.Kernel.Pagination;

public interface IPaginationData
{
    int PageNumber { get; }
    int PageSize { get; }
    string SearchTerm { get; }
    string SortByColumn { get; }
    SortOrder SortOrder { get; }
}
