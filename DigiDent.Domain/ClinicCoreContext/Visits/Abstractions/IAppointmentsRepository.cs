using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;

public interface IAppointmentsRepository
    : IVisitsRepository<Appointment, AppointmentId, Guid>
{
    Task DeleteAsync(AppointmentId id, CancellationToken cancellationToken);
    Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken);
}