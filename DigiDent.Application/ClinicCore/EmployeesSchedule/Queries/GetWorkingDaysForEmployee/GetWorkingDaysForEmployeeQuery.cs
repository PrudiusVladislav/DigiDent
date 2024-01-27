using DigiDent.Application.Shared.Abstractions;

namespace DigiDent.Application.ClinicCore.EmployeesSchedule.Queries.GetWorkingDaysForEmployee;

public sealed record GetWorkingDaysForEmployeeQuery(
    Guid EmployeeId,
    DateOnly From,
    DateOnly Until) : IQuery<IReadOnlyCollection<WorkingDayDTO>>;