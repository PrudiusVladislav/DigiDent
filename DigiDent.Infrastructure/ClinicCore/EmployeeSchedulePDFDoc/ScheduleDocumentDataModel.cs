using DigiDent.Application.ClinicCore.EmployeesSchedule.Queries.GetWorkingDaysForEmployee;

namespace DigiDent.Infrastructure.ClinicCore.EmployeeSchedulePDFDoc;

public sealed record ScheduleDocumentDataModel(
    string ClinicName,
    string EmployeeFullName,
    DateOnly FromDate,
    DateOnly ToDate,
    List<WorkingDayDTO> WorkingDays);