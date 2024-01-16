using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;

public record PastVisitId(Guid Value): IVisitId<Guid>;