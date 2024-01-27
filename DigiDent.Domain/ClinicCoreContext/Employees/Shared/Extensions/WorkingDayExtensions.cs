
namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.Extensions;

public static class WorkingDayExtensions
{
    /// <summary>
    /// Gets working days from the given collection that are in the requested date range.
    /// </summary>
    /// <param name="workingDays">The working days collection.</param>
    /// <param name="fromDate">The start date.</param>
    /// <param name="untilDate">The end date.</param>
    public static IOrderedEnumerable<WorkingDay> GetWorkingDaysBetweenDates(
        this IEnumerable<WorkingDay> workingDays,
        DateOnly fromDate,
        DateOnly untilDate)
    {
        return workingDays
            .Where(wd => wd.Date >= fromDate && wd.Date <= untilDate)
            .OrderBy(wd => wd.Date);
    } 
}