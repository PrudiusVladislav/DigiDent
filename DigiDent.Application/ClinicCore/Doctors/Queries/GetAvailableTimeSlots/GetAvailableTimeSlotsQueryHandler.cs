﻿using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.GetAvailableTimeSlots;

public class GetAvailableTimeSlotsQueryHandler
    : IQueryHandler<GetAvailableTimeSlotsQuery, IReadOnlyCollection<DateTime>>
{
    private readonly IDoctorsRepository _doctorsRepository;

    public GetAvailableTimeSlotsQueryHandler(IDoctorsRepository doctorsRepository)
    {
        _doctorsRepository = doctorsRepository;
    }

    public async Task<IReadOnlyCollection<DateTime>> Handle(
        GetAvailableTimeSlotsQuery request, CancellationToken cancellationToken)
    {
        var doctorId = new EmployeeId(request.DoctorId);
        var doctor = await _doctorsRepository.GetByIdAsync(
            doctorId,
            includeScheduling: true,
            cancellationToken: cancellationToken);
        
        if (doctor is null) return Array.Empty<DateTime>();
        
        IReadOnlyCollection<DateTime> timeSlots = doctor.GetAvailableTimeSlots(
            request.FromDateTime,
            request.UntilDate,
            TimeSpan.FromMinutes(request.DurationInMinutes));

        return timeSlots;
    }
}