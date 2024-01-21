using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;

namespace DigiDent.Application.ClinicCore.Doctors.Queries.IsDoctorAvailable;

public class IsDoctorAvailableQueryHandler
    : IQueryHandler<IsDoctorAvailableQuery, IsDoctorAvailableResponse>
{
    private readonly IDoctorsRepository _doctorsRepository;

    public IsDoctorAvailableQueryHandler(IDoctorsRepository doctorsRepository)
    {
        _doctorsRepository = doctorsRepository;
    }

    public async Task<IsDoctorAvailableResponse> Handle(
        IsDoctorAvailableQuery request, CancellationToken cancellationToken)
    {
        var doctorId = new EmployeeId(request.DoctorId);
        var doctor = await _doctorsRepository.GetByIdAsync(
            doctorId,
            includeScheduling: true,
            cancellationToken: cancellationToken);
        
        if (doctor is null) return new IsDoctorAvailableResponse(false);
        
        bool isAvailable = doctor.IsAvailableAt(
            request.DateTime,
            DateTime.Now, 
            TimeSpan.FromMinutes(request.DurationInMinutes));
        
        return new IsDoctorAvailableResponse(isAvailable);
    }
}