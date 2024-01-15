using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;

public record DentalProcedureId(Guid Value): ITypedId<Guid>;