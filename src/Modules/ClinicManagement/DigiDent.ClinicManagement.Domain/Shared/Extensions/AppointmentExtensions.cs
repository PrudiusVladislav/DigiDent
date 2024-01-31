using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;

namespace DigiDent.Domain.ClinicCoreContext.Shared.Extensions;

public static class AppointmentExtensions
{
    /// <summary>
    /// Returns an ordered collection of appointments on a given working day.
    /// </summary>
    /// <param name="appointments">The source collection of appointments.</param>
    /// <param name="workingDay">The working day.</param>
    /// <returns></returns>
    internal static IOrderedEnumerable<Appointment> GetAppointmentsOnWorkingDay(
        this IEnumerable<Appointment> appointments,
        WorkingDay workingDay)
    {
        return appointments
            .Where(a =>
                a.VisitDateTime.Value.Date.ToDateOnly() == workingDay.Date)
            .OrderBy(a => a.VisitDateTime);
    }
    
    /// <summary>
    /// Converts a collection of appointments to a collection of <see cref="EventTimeNode"/>s.
    /// </summary>
    /// <param name="appointments">The source collection of appointments.</param>
    /// <returns></returns>
    internal static IEnumerable<EventTimeNode> ConvertToEventTimeNodes(
        this IEnumerable<Appointment> appointments)
    {
        return appointments
            .Select(a => new EventTimeNode(
                TimeOnly.FromDateTime(a.VisitDateTime.Value),
                a.Duration.Duration));
    }
}