using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;

public record SchedulePreferenceId(Guid Value): TypedId<Guid>(Value);