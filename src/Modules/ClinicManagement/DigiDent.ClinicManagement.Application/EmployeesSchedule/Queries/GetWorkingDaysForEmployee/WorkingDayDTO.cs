namespace DigiDent.ClinicManagement.Application.EmployeesSchedule.Queries.GetWorkingDaysForEmployee;

public class WorkingDayDTO
{
    public Guid Id { get; init; }
    public string EmployeeFullName { get; init; } = string.Empty;
    public DateOnly Date { get; init; }
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }
}