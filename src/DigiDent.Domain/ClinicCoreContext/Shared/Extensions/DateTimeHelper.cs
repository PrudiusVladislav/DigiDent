namespace DigiDent.Domain.ClinicCoreContext.Shared.Extensions;

public static class DateTimeHelper
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
    /// Converts <see cref="DateOnly"/> and <see cref="TimeOnly"/> to <see cref="DateTime"/>
    /// </summary>
    /// <param name="date">The date only.</param>
    /// <param name="time">The time only.</param>
    /// <returns></returns>
    public static DateTime ToDateTime(this DateOnly date, TimeOnly time)
    {
        return DateTime.Parse($"{date} {time}");
    }
}