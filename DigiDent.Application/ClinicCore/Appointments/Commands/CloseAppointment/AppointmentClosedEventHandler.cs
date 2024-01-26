using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Visits.Events;
using MediatR;

namespace DigiDent.Application.ClinicCore.Appointments.Commands.CloseAppointment;

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
        PastVisit pastVisit = PastVisit.Create(
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