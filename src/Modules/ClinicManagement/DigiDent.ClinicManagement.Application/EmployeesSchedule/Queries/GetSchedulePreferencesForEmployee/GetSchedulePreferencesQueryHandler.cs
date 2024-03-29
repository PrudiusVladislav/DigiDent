﻿using AutoMapper;
using DigiDent.ClinicManagement.Domain.Employees.Shared;
using DigiDent.ClinicManagement.Domain.Employees.Shared.Abstractions;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.Shared.Abstractions.Queries;

namespace DigiDent.ClinicManagement.Application.EmployeesSchedule.Queries.GetSchedulePreferencesForEmployee;

public sealed class GetSchedulePreferencesQueryHandler
    : IQueryHandler<GetSchedulePreferencesQuery, IReadOnlyCollection<SchedulePreferenceDTO>>
{
    private readonly IAllEmployeesRepository _allEmployeesRepository;
    private readonly IMapper _mapper;
    
    public GetSchedulePreferencesQueryHandler(
        IAllEmployeesRepository allEmployeesRepository,
        IMapper mapper)
    {
        _allEmployeesRepository = allEmployeesRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<SchedulePreferenceDTO>> Handle(
        GetSchedulePreferencesQuery query, CancellationToken cancellationToken)
    {
        EmployeeId employeeId = new(query.EmployeeId);
        
        Employee? employee = await _allEmployeesRepository.GetByIdAsync(
            employeeId,
            includeScheduling: true,
            cancellationToken: cancellationToken);
        
        if (employee is null) 
            return Array.Empty<SchedulePreferenceDTO>();
        
        return employee.SchedulePreferences
            .Select(_mapper.Map<SchedulePreferenceDTO>)
            .ToList()
            .AsReadOnly();
    }
}