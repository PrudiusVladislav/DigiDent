namespace DigiDent.Shared.Kernel.ValueObjects.Pagination;

public interface IPaginationOptions
{
    int PageNumber { get; }
    int PageSize { get; }
    string SearchTerm { get; }
    string SortByColumn { get; }
    SortOrder SortOrder { get; }
}
