using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;

public record TreatmentPlanId(Guid Value): ITypedId<Guid>;