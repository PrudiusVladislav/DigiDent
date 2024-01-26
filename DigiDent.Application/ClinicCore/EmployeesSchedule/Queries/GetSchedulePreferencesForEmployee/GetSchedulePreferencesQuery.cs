using DigiDent.Application.Shared.Abstractions;

namespace DigiDent.Application.ClinicCore.EmployeesSchedule.Queries.GetSchedulePreferencesForEmployee;

public sealed record GetSchedulePreferencesQuery(Guid EmployeeId)
    : IQuery<IReadOnlyCollection<SchedulePreferenceDTO>>;