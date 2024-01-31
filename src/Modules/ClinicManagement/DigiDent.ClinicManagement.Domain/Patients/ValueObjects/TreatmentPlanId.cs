using DigiDent.Shared.Domain.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;

public record TreatmentPlanId(Guid Value): ITypedId<Guid>;