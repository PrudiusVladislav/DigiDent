namespace DigiDent.ClinicManagement.Application.EmployeesSchedule.Commands.AddWorkingDay;

public sealed record AddWorkingDayRequest(
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime);
    