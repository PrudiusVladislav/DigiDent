using AutoMapper;
using DigiDent.ClinicManagement.Application.EmployeesSchedule.Queries.GetSchedulePreferencesForEmployee;
using DigiDent.ClinicManagement.Application.EmployeesSchedule.Queries.GetWorkingDaysForEmployee;
using DigiDent.ClinicManagement.Domain.Employees.Shared;

namespace DigiDent.ClinicManagement.Application.EmployeesSchedule.Queries;

public class ScheduleMappingProfiles: Profile
{
    public ScheduleMappingProfiles()
    {
        CreateMap<WorkingDay, WorkingDayDTO>()
            .ForMember(
                wd => wd.Start,
                opt => opt.MapFrom(src => src.StartEndTime.StartTime))
            .ForMember(
                wd => wd.End,
                opt => opt.MapFrom(src => src.StartEndTime.EndTime))
            .ForMember(
                wd => wd.EmployeeFullName,
                opt => opt.MapFrom(src => src.Employee.FullName));
        CreateMap<SchedulePreference, SchedulePreferenceDTO>();
    }
}