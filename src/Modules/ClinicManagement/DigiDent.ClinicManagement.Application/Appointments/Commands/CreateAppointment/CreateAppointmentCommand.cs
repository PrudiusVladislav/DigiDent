using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Appointments.Commands.CreateAppointment;

public sealed record CreateAppointmentCommand
    : ICommand<Result<Guid>>
{
    public EmployeeId DoctorId { get; init; } = null!;
    public PatientId PatientId { get; init; } = null!;
    public VisitDateTime DateTime { get; init; } = null!;
    public TimeDuration Duration { get; init; } = null!;
    public IEnumerable<ProvidedServiceId> ServicesIds { get; init; } 
        = Array.Empty<ProvidedServiceId>();
    
    public static Result<CreateAppointmentCommand> CreateFromRequest(
        CreateAppointmentRequest request, IDateTimeProvider dateTimeProvider)
    {
        Result<VisitDateTime> dateTimeResult = VisitDateTime.Create(
            request.DateTime, dateTimeProvider);
        
        Result<TimeDuration> timeResult = TimeDuration.Create(TimeSpan
            .FromMinutes(request.Duration));
        
        Result validationResult = Result.Merge(dateTimeResult, timeResult);
        
        if (validationResult.IsFailure)
            return validationResult.MapToType<CreateAppointmentCommand>();

        return Result.Ok(new CreateAppointmentCommand()
        {
            DoctorId = new EmployeeId(request.DoctorId),
            PatientId = new PatientId(request.PatientId),
            DateTime = dateTimeResult.Value!,
            Duration = timeResult.Value!,
            ServicesIds = request.Services
                .Select(id => new ProvidedServiceId(id))
        });
    }
}