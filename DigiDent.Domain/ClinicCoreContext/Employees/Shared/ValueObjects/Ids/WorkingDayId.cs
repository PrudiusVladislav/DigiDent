using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;

public record WorkingDayId(Guid Value): TypedId<Guid>(Value);