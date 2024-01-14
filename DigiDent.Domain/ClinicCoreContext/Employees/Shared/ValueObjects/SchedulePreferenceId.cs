using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;

public record SchedulePreferenceId(Guid Value): TypedId<Guid>(Value);