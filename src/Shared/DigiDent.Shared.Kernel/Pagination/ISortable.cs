
namespace DigiDent.Shared.Kernel.Pagination;

public interface ISortable
{
    IComparable GetDefaultSortProperty();
}