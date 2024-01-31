using DigiDent.Shared.Domain.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;

public record ProvidedServiceId(Guid Value): ITypedId<Guid>;