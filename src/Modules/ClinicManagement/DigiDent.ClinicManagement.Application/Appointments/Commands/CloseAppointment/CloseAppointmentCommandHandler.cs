using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Appointments.Commands.CloseAppointment;

public sealed class CloseAppointmentCommandHandler
    : ICommandHandler<CloseAppointmentCommand, Result>
{
    private readonly IAppointmentsRepository _appointmentsRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    public CloseAppointmentCommandHandler(
        IAppointmentsRepository appointmentsRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _appointmentsRepository = appointmentsRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(
        CloseAppointmentCommand command, CancellationToken cancellationToken)
    {
        Appointment? appointment = await _appointmentsRepository.GetByIdAsync(
            command.AppointmentId, cancellationToken);
        
        if (appointment is null) 
            return Result.Fail(RepositoryErrors
                .EntityNotFound<Appointment, Guid>(command.AppointmentId));
        
        Result closeResult = appointment.Close(
            command.ClosureStatus, command.Price, _dateTimeProvider);
        
        if (closeResult.IsFailure)
            return closeResult;
        
        await _appointmentsRepository.DeleteAsync(appointment.Id, cancellationToken);
        return Result.Ok();
    }
}