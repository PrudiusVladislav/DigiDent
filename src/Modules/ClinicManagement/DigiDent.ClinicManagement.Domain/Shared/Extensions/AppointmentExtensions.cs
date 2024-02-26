using DigiDent.ClinicManagement.Domain.Employees.Shared;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.Shared.Kernel.Extensions;

namespace DigiDent.ClinicManagement.Domain.Shared.Extensions;

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
            .Select(a =>
                new EventTimeNode(
                    startTime: TimeOnly.FromDateTime(a.VisitDateTime.Value),
                    duration: a.Duration.Duration));
    }
}