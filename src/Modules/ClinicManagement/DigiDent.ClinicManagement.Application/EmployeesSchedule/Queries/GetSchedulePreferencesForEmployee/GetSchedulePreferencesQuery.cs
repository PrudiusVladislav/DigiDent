using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.EmployeesSchedule.Queries.GetSchedulePreferencesForEmployee;

public sealed record GetSchedulePreferencesQuery(Guid EmployeeId)
    : IQuery<IReadOnlyCollection<SchedulePreferenceDTO>>;