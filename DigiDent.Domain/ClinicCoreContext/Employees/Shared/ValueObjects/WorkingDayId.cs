using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;

public record WorkingDayId(Guid Value): TypedId<Guid>(Value);