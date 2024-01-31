using DigiDent.ClinicManagement.Domain.Visits.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;

public record PastVisitId(Guid Value): IVisitId<Guid>;