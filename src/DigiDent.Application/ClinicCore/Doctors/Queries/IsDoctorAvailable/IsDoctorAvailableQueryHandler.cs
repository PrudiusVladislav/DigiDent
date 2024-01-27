using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.IsDoctorAvailable;

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