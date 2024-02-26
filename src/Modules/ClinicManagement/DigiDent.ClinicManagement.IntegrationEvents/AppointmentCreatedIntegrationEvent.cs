using DigiDent.Shared.Abstractions.Messaging;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.ClinicManagement.IntegrationEvents;

public class AppointmentCreatedIntegrationEvent: IIntegrationEvent
{
    public DateTime TimeOfOccurrence { get; init; }
    public string PatientEmail { get; init; } = null!;
    public string PatientFullName { get; init; } = null!;
    public DateTime ArrangedDateTime { get; init; }
    public string DoctorFullName { get; init; } = null!;
}
    