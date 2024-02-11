using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Appointments.Commands.AddAppointmentMediaFiles;

public static class MediaAttachmentsErrors
{
    public static Error NoFilesToUpload
        => new(
            ErrorType.Validation,
            nameof(AddAppointmentMediaFilesCommandHandler),
            "No media files attachments were provided to upload.");
    
    public static Error FilesOfInvalidType(string[] allowedExtensions)
        => new(
            ErrorType.Validation,
            nameof(AddAppointmentMediaFilesCommandHandler),
            $"Some of the media files attachments are of invalid type." + 
            $" Allowed extensions: {string.Join(", ", allowedExtensions)}");
}