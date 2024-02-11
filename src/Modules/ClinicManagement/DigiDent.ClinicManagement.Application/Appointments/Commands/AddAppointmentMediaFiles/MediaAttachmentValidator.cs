using Microsoft.AspNetCore.Http;

namespace DigiDent.ClinicManagement.Application.Appointments.Commands.AddAppointmentMediaFiles;

public class MediaAttachmentValidator
{
    public static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".docx"];
    
    public static bool FileIsAllowed(IFormFile file)
    {
        string fileExtension = Path.GetExtension(file.FileName).ToLower();
        return AllowedExtensions.Contains(fileExtension);
    }
}