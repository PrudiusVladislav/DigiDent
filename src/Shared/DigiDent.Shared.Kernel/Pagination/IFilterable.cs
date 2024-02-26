namespace DigiDent.Shared.Kernel.Pagination;

public interface IFilterable
{
    bool Contains(string value);
}