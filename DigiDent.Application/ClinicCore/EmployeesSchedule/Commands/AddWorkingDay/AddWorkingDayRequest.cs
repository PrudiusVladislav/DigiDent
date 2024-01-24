namespace DigiDent.Application.ClinicCore.EmployeesSchedule.Commands.AddWorkingDay;

public record AddWorkingDayRequest(
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime);
    