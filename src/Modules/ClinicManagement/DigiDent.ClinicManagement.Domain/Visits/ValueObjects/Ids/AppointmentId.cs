using DigiDent.ClinicManagement.Domain.Visits.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;

public record AppointmentId(Guid Value): IVisitId<Guid>;