using AutoMapper;
using DigiDent.Application.ClinicCore.EmployeesSchedule.Queries.GetSchedulePreferencesForEmployee;
using DigiDent.Application.ClinicCore.EmployeesSchedule.Queries.GetWorkingDaysForEmployee;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared;

namespace DigiDent.Application.ClinicCore.EmployeesSchedule.Queries;

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