using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;

public record AppointmentId(Guid Value): ITypedId<Guid>;