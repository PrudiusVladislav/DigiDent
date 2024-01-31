﻿using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Abstractions.Queries;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Doctors.Queries.IsDoctorAvailable;

public sealed class IsDoctorAvailableQueryHandler
    : IQueryHandler<IsDoctorAvailableQuery, Result<IsDoctorAvailableResponse>>
{
    private readonly IDoctorsRepository _doctorsRepository;

    public IsDoctorAvailableQueryHandler(IDoctorsRepository doctorsRepository)
    {
        _doctorsRepository = doctorsRepository;
    }

    public async Task<Result<IsDoctorAvailableResponse>> Handle(
        IsDoctorAvailableQuery query, CancellationToken cancellationToken)
    {
        Result<TimeDuration> timeResult = TimeDuration.Create(TimeSpan
            .FromMinutes(query.DurationInMinutes));
        
        if (timeResult.IsFailure)
            return timeResult.MapToType<IsDoctorAvailableResponse>();
        
        EmployeeId doctorId = new(query.DoctorId);
        
        Doctor? doctor = await _doctorsRepository.GetByIdAsync(
            doctorId,
            includeScheduling: true,
            cancellationToken: cancellationToken);
        
        if (doctor is null) 
            return Result.Fail<IsDoctorAvailableResponse>(RepositoryErrors
                .EntityNotFound<Doctor>(doctorId.Value));
        
        bool isAvailable = doctor.IsAvailableAt(
            query.DateTime,
            currentDateTime: DateTime.Now, 
            timeResult.Value!);
        
        return Result.Ok(new IsDoctorAvailableResponse(isAvailable));
    }
}