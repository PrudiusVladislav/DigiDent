using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.ClinicManagement.Domain.Visits.Events;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using MediatR;

namespace DigiDent.ClinicManagement.Application.Appointments.Commands.CloseAppointment;

public sealed class AppointmentClosedEventHandler
    : INotificationHandler<AppointmentClosedDomainEvent>
{
    private readonly IPastVisitsRepository _pastVisitsRepository;

    public AppointmentClosedEventHandler(IPastVisitsRepository pastVisitsRepository)
    {
        _pastVisitsRepository = pastVisitsRepository;
    }

    public async Task Handle(
        AppointmentClosedDomainEvent notification, CancellationToken cancellationToken)
    {
        PastVisit pastVisit = new(
            new PastVisitId(notification.ClosedAppointment.Id.Value),
            notification.ClosedAppointment.DoctorId,
            notification.ClosedAppointment.PatientId,
            notification.ClosedAppointment.VisitDateTime,
            notification.PricePaid,
            notification.ClosureStatus,
            proceduresDone: notification.ClosedAppointment.ProvidedServices
                .Select(ps => ps.Details.Name).ToList(),
            notification.ClosedAppointment.TreatmentPlanId);

        await _pastVisitsRepository.AddAsync(pastVisit, cancellationToken);
    }
}