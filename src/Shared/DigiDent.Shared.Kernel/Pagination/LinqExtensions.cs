using System.Reflection;

namespace DigiDent.Shared.Kernel.Pagination;

public static class LinqExtensions
{
    /// <summary>
    /// Filters the items based on the search term.
    /// </summary>
    /// <param name="items"> Items to filter. </param>
    /// <param name="searchTerm"> Search term. If empty, returns the items as is. </param>
    /// <typeparam name="T"> Type of the IFilterable items. </typeparam>
    /// <returns></returns>
    public static IEnumerable<T> Filter<T>(
        this IEnumerable<T> items, string searchTerm)
        where T : IFilterable
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return items;
        
        return items.Where(item => item.Contains(searchTerm));
    }
    
    /// <summary>
    /// Provides a way to sort the items based on the <see cref="SortOrder"/> value.
    /// </summary>
    public static IOrderedEnumerable<TSource> SortBy<TSource, TKey>(
        this IEnumerable<TSource> items, Func<TSource, TKey> orderBy, SortOrder sortOrder)
    {
        return sortOrder == SortOrder.Desc 
            ? items.OrderByDescending(orderBy)
            : items.OrderBy(orderBy);
    }
    
    /// <summary>
    /// Sorts the items based on the column name and <see cref="SortOrder"/> value.
    /// </summary>
    /// <param name="items"> Items to sort. </param>
    /// <param name="sortByColumn"> Column name to sort by. </param>
    /// <param name="sortOrder"> Sort order. </param>
    /// <returns></returns>
    public static IOrderedEnumerable<T> SortBy<T>(
        this IEnumerable<T> items, 
        string sortByColumn,
        SortOrder sortOrder)
        where T : ISortable
    {
        if (typeof(T).TryGetSortableProperty(sortByColumn,
                out PropertyInfo? orderByProperty))
        {
            return items.SortBy(orderByProperty!, sortOrder);
        }
        
        return items.SortBy(t => t.DefaultSortProperty, sortOrder);
    }
    
    private static IOrderedEnumerable<T> SortBy<T>(
        this IEnumerable<T> items, PropertyInfo orderColumn, SortOrder sortOrder)
    {
        return items.SortBy(item => orderColumn.GetValue(item), sortOrder);
    }
    
    private static bool TryGetSortableProperty(
        this Type itemType, string propertyName, out PropertyInfo? property)
    {
        property = null;
        
        if (string.IsNullOrWhiteSpace(propertyName))
            return false;
        
        PropertyInfo? propertyToCheck = itemType.GetProperty(propertyName, 
            BindingFlags.IgnoreCase | BindingFlags.Public);
        if (propertyToCheck is null)
            return false;

        bool isComparable = propertyToCheck.PropertyType
            .GetInterfaces()
            .All(i =>
                i == typeof(IComparable) ||
                i == typeof(IComparable<>)
                    .MakeGenericType(propertyToCheck.PropertyType));
        
        bool isSortable = propertyToCheck
            .GetCustomAttributes()
            .All(a => a is not NotSortableAttribute);
        
        if (!isComparable || !isSortable)
            return false;
        
        property = propertyToCheck;
        return true;
    }
}