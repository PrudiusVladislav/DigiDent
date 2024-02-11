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
    private readonly IMediaFilesS3Repository _mediaFilesS3Repository;

    public AddAppointmentMediaFilesCommandHandler(
        IAppointmentsRepository appointmentsRepository,
        IMediaFilesS3Repository mediaFilesS3Repository)
    {
        _appointmentsRepository = appointmentsRepository;
        _mediaFilesS3Repository = mediaFilesS3Repository;
    }

    public async Task<Result> Handle(
        AddAppointmentMediaFilesCommand command, CancellationToken cancellationToken)
    {
        if (command.MediaFiles.Count == 0)
        {
            return Result.Fail(MediaAttachmentsErrors.NoFilesToUpload);
        }
        
        if (command.MediaFiles.Any(file =>
                !MediaAttachmentValidator.FileIsAllowed(file)))
        {
            return Result.Fail(MediaAttachmentsErrors
                .FilesOfInvalidType(MediaAttachmentValidator.AllowedExtensions));
        }
        
        AppointmentId appointmentId = new(command.AppointmentId);
        Appointment? appointment = await _appointmentsRepository.GetByIdAsync(
            appointmentId, cancellationToken);
        
        if (appointment is null)
            return Result.Fail(RepositoryErrors
                .EntityNotFound<Appointment>(appointmentId.Value));
        
        await _mediaFilesS3Repository.UploadMediaFilesAsync(
            S3Keys.BucketName,
            S3Keys.VisitAttachedMediaKey(appointment.Id),
            command.MediaFiles,
            cancellationToken);

        return Result.Ok();
    }
}