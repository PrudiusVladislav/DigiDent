namespace DigiDent.Application.ClinicCore.EmployeesSchedule.Commands.AddWorkingDay;

public sealed record AddWorkingDayRequest(
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime);
    