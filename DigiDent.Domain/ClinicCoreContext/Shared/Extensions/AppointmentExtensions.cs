using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;

namespace DigiDent.Domain.ClinicCoreContext.Shared.Extensions;

public static class AppointmentExtensions
{
    internal static IOrderedEnumerable<Appointment> GetAppointmentsOnWorkingDay(
        this IEnumerable<Appointment> appointments,
        WorkingDay workingDay)
    {
        return appointments
            .Where(a => DateOnly.FromDateTime(
                a.VisitDateTime.Date)== workingDay.Date)
            .OrderBy(a => a.VisitDateTime);
    }

    internal static IEnumerable<EventTimeNode> ConvertToEventTimeNodes(
        this IEnumerable<Appointment> appointments)
    {
        return appointments
            .Select(a => new EventTimeNode(
                TimeOnly.FromDateTime(a.VisitDateTime),
                a.Duration));
    }
}