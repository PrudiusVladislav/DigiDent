using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.EmployeesSchedule.Queries.GetWorkingDaysForEmployee;

public sealed record GetWorkingDaysForEmployeeQuery(
    Guid EmployeeId,
    DateOnly From,
    DateOnly Until) : IQuery<IReadOnlyCollection<WorkingDayDTO>>;