using DigiDent.ClinicManagement.Application.Abstractions;
using DigiDent.ClinicManagement.Application.Constants;
using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Appointments.Commands.AddAppointmentMediaFiles;

public sealed class AddAppointmentMediaFilesCommandHandler
    : ICommandHandler<AddAppointmentMediaFilesCommand, Result>
{
    private readonly IAppointmentsRepository _appointmentsRepository;
    private readonly IMediaFilesS3Service _mediaFilesS3Service;

    public AddAppointmentMediaFilesCommandHandler(
        IAppointmentsRepository appointmentsRepository,
        IMediaFilesS3Service mediaFilesS3Service)
    {
        _appointmentsRepository = appointmentsRepository;
        _mediaFilesS3Service = mediaFilesS3Service;
    }

    public async Task<Result> Handle(
        AddAppointmentMediaFilesCommand command, CancellationToken cancellationToken)
    {
        AppointmentId appointmentId = new(command.AppointmentId);
        Appointment? appointment = await _appointmentsRepository.GetByIdAsync(
            appointmentId, cancellationToken);
        
        if (appointment is null)
            return Result.Fail(RepositoryErrors
                .EntityNotFound<Appointment>(appointmentId.Value));
        
        await _mediaFilesS3Service.UploadMediaFilesAsync(
            S3Keys.BucketName,
            S3Keys.VisitAttachedMediaKey(appointment.Id),
            command.MediaFiles,
            cancellationToken);

        return Result.Ok();
    }
}