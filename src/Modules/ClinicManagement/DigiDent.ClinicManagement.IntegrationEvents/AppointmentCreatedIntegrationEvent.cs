using DigiDent.Shared.Abstractions.Messaging;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.ClinicManagement.IntegrationEvents;

public record AppointmentCreatedIntegrationEvent(
    Guid Id,
    DateTime TimeOfOccurrence,
    Email PatientEmail,
    FullName PatientFullName,
    DateTime ArrangedDateTime,
    FullName DoctorFullName) : IIntegrationEvent;