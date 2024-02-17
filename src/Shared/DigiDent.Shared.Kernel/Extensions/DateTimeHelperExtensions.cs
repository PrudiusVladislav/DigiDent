namespace DigiDent.Shared.Kernel.Extensions;

public static class DateTimeHelperExtensions
{
    /// <summary>
    /// Converts given <see cref="DateTime"/> to <see cref="DateOnly"/>
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static DateOnly ToDateOnly(this DateTime dateTime)
    {
        return DateOnly.FromDateTime(dateTime);
    }
    
    /// <summary>
    /// Converts given <see cref="DateTime"/> to <see cref="TimeOnly"/>
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <returns></returns>
    public static TimeOnly ToTimeOnly(this DateTime dateTime)
    {
        return TimeOnly.FromDateTime(dateTime);
    }
}