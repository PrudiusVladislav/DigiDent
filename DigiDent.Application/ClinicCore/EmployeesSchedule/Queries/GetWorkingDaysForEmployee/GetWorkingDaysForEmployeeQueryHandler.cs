using AutoMapper;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Extensions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;

namespace DigiDent.Application.ClinicCore.EmployeesSchedule.Queries.GetWorkingDaysForEmployee;

public sealed class GetWorkingDaysForEmployeeQueryHandler
    : IQueryHandler<GetWorkingDaysForEmployeeQuery, IReadOnlyCollection<WorkingDayDTO>>
{
    private readonly IAllEmployeesRepository _allEmployeesRepository;
    private readonly IMapper _mapper;

    public GetWorkingDaysForEmployeeQueryHandler(
        IAllEmployeesRepository allEmployeesRepository,
        IMapper mapper)
    {
        _allEmployeesRepository = allEmployeesRepository;
        _mapper = mapper;
    }
    
    public async Task<IReadOnlyCollection<WorkingDayDTO>> Handle(
        GetWorkingDaysForEmployeeQuery query, CancellationToken cancellationToken)
    {
        EmployeeId employeeId = new(query.EmployeeId);
        Employee? employee = await _allEmployeesRepository.GetByIdAsync(
            employeeId,
            includeScheduling: true,
            cancellationToken: cancellationToken);
        
        if (employee is null)
            return Array.Empty<WorkingDayDTO>();
        
        return employee.WorkingDays
            .GetWorkingDaysBetweenDates(query.From, query.Until)
            .Select(_mapper.Map<WorkingDayDTO>)
            .ToList()
            .AsReadOnly();
    }
}