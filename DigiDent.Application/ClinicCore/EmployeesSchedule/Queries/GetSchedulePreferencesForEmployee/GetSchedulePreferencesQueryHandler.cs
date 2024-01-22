using AutoMapper;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;

namespace DigiDent.Application.ClinicCore.EmployeesSchedule.Queries.GetSchedulePreferencesForEmployee;

public class GetSchedulePreferencesQueryHandler
    : IQueryHandler<GetSchedulePreferencesQuery, IReadOnlyCollection<SchedulePreferenceDTO>>
{
    private readonly IAllEmployeesRepository _allEmployeesRepository;
    private readonly IMapper _mapper;
    
    public GetSchedulePreferencesQueryHandler(
        IAllEmployeesRepository allEmployeesRepository, IMapper mapper)
    {
        _allEmployeesRepository = allEmployeesRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<SchedulePreferenceDTO>> Handle(
        GetSchedulePreferencesQuery request, CancellationToken cancellationToken)
    {
        var employeeId = new EmployeeId(request.EmployeeId);
        var employee = await _allEmployeesRepository.GetByIdAsync(
            employeeId,
            includeScheduling: true,
            cancellationToken: cancellationToken);
        
        if (employee is null) return Array.Empty<SchedulePreferenceDTO>();
        
        return employee.SchedulePreferences
            .Select(_mapper.Map<SchedulePreferenceDTO>)
            .ToList()
            .AsReadOnly();
    }
}