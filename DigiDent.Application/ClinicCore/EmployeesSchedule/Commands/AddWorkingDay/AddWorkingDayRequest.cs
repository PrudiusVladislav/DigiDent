namespace DigiDent.Application.ClinicCore.EmployeesSchedule.Commands.AddWorkingDay;

public record AddWorkingDayRequest(
    DateOnly Date,
    TimeOnly Start,
    TimeOnly End);
    
    //example json request
    // {
    //     "date": "2021-10-10",
    //     "start": "10:00",
    //     "end": "18:00"