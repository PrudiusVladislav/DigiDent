using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.IsDoctorAvailable;

public class IsDoctorAvailableQueryHandler
    : IQueryHandler<IsDoctorAvailableQuery, Result<IsDoctorAvailableResponse>>
{
    private readonly IDoctorsRepository _doctorsRepository;

    public IsDoctorAvailableQueryHandler(IDoctorsRepository doctorsRepository)
    {
        _doctorsRepository = doctorsRepository;
    }

    public async Task<Result<IsDoctorAvailableResponse>> Handle(
        IsDoctorAvailableQuery request, CancellationToken cancellationToken)
    {
        var timeResult = TimeDuration.Create(TimeSpan
            .FromMinutes(request.DurationInMinutes));
        if (timeResult.IsFailure)
            return timeResult.MapToType<IsDoctorAvailableResponse>();
        
        var doctorId = new EmployeeId(request.DoctorId);
        var doctor = await _doctorsRepository.GetByIdAsync(
            doctorId,
            includeScheduling: true,
            cancellationToken: cancellationToken);
        
        if (doctor is null) 
            return Result.Fail<IsDoctorAvailableResponse>(RepositoryErrors
                .EntityNotFound<Doctor>(doctorId.Value));
        
        bool isAvailable = doctor.IsAvailableAt(
            request.DateTime,
            DateTime.Now, 
            timeResult.Value!);
        
        return Result.Ok(new IsDoctorAvailableResponse(isAvailable));
    }
}