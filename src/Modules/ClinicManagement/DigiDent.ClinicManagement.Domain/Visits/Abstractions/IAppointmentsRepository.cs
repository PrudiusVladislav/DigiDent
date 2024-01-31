using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;

namespace DigiDent.ClinicManagement.Domain.Visits.Abstractions;

public interface IAppointmentsRepository
    : IVisitsRepository<Appointment, AppointmentId, Guid>
{
    Task DeleteAsync(AppointmentId id, CancellationToken cancellationToken);
    Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken);
}