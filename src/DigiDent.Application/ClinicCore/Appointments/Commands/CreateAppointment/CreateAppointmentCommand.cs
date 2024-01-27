using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Appointments.Commands.CreateAppointment;

public sealed record CreateAppointmentCommand
    : ICommand<Result<Guid>>
{
    public EmployeeId DoctorId { get; init; } = null!;
    public PatientId PatientId { get; init; } = null!;
    public DateTime DateTime { get; init; }
    public TimeDuration Duration { get; init; } = null!;
    public IEnumerable<ProvidedServiceId> ServicesIds { get; init; } 
        = Array.Empty<ProvidedServiceId>();
    
    public static Result<CreateAppointmentCommand> CreateFromRequest(
        CreateAppointmentRequest request)
    {
        Result<TimeDuration> timeResult = TimeDuration.Create(TimeSpan
            .FromMinutes(request.Duration));
        
        if (timeResult.IsFailure)
            return timeResult.MapToType<CreateAppointmentCommand>();

        return Result.Ok(new CreateAppointmentCommand()
        {
            DoctorId = new EmployeeId(request.DoctorId),
            PatientId = new PatientId(request.PatientId),
            DateTime = request.DateTime,
            Duration = timeResult.Value!,
            ServicesIds = request.Services
                .Select(id => new ProvidedServiceId(id))
        });
    }
}