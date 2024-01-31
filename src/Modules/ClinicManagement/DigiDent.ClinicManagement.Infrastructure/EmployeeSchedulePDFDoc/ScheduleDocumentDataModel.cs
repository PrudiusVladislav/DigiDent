using DigiDent.ClinicManagement.Application.EmployeesSchedule.Queries.GetWorkingDaysForEmployee;

namespace DigiDent.ClinicManagement.Infrastructure.EmployeeSchedulePDFDoc;

public sealed record ScheduleDocumentDataModel(
    string ClinicName,
    string EmployeeFullName,
    DateOnly FromDate,
    DateOnly ToDate,
    List<WorkingDayDTO> WorkingDays);