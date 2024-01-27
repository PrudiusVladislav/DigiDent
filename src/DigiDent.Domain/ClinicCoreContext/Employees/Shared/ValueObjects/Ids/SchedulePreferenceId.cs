using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;

public record SchedulePreferenceId(Guid Value): TypedId<Guid>(Value);