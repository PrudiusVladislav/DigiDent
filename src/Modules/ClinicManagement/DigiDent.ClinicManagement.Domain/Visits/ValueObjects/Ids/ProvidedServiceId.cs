using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;

public record ProvidedServiceId(Guid Value): ITypedId<Guid>;