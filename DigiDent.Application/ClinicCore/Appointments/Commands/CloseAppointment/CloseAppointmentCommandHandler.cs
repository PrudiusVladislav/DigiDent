using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Appointments.Commands.CloseAppointment;

public sealed class CloseAppointmentCommandHandler
    : ICommandHandler<CloseAppointmentCommand, Result>
{
    private readonly IAppointmentsRepository _appointmentsRepository;

    public CloseAppointmentCommandHandler(IAppointmentsRepository appointmentsRepository)
    {
        _appointmentsRepository = appointmentsRepository;
    }

    public async Task<Result> Handle(
        CloseAppointmentCommand command, CancellationToken cancellationToken)
    {
        Appointment? appointment = await _appointmentsRepository.GetByIdAsync(
            command.AppointmentId, cancellationToken);
        
        if (appointment is null) 
            return Result.Fail(RepositoryErrors
                .EntityNotFound<Appointment>(command.AppointmentId.Value));
        
        Result closeResult = appointment.Close(command.ClosureStatus, command.Price);
        if (closeResult.IsFailure)
            return closeResult;
        
        await _appointmentsRepository.DeleteAsync(appointment.Id, cancellationToken);
        return Result.Ok();
    }
}