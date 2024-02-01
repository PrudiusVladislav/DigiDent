using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.Shared.Abstractions.Queries;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Doctors.Queries.GetAvailableTimeSlots;

public sealed class GetAvailableTimeSlotsQueryHandler
    : IQueryHandler<GetAvailableTimeSlotsQuery, IReadOnlyCollection<DateTime>>
{
    private readonly IDoctorsRepository _doctorsRepository;

    public GetAvailableTimeSlotsQueryHandler(IDoctorsRepository doctorsRepository)
    {
        _doctorsRepository = doctorsRepository;
    }

    public async Task<IReadOnlyCollection<DateTime>> Handle(
        GetAvailableTimeSlotsQuery query, CancellationToken cancellationToken)
    {
        Result<TimeDuration> timeResult = TimeDuration.Create(TimeSpan
            .FromMinutes(query.DurationInMinutes));
        
        if (timeResult.IsFailure) 
            return Array.Empty<DateTime>();
        
        EmployeeId doctorId = new(query.DoctorId);
        
        Doctor? doctor = await _doctorsRepository.GetByIdAsync(
            doctorId,
            includeScheduling: true,
            cancellationToken: cancellationToken);
        
        if (doctor is null) 
            return Array.Empty<DateTime>();
        
        IReadOnlyCollection<DateTime> timeSlots = doctor.GetAvailableTimeSlots(
            query.FromDateTime,
            query.UntilDate,
            DateTime.Now, 
            timeResult.Value!);

        return timeSlots;
    }
}