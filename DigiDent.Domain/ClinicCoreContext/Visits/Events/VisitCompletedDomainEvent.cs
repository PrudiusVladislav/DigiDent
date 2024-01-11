using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Events;

public record VisitCompletedDomainEvent(
    Guid Id,
    DateTime TimeOfOccurence,
    VisitId VisitId,
    string DoctorEmail,
    string PatientEmail) : DomainEvent(Id, TimeOfOccurence);