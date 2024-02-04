namespace DigiDent.Notification.Infrastructure.EmailTemplates;

public class AppointmentArrangedEmailTemplate
{
    public static string Create(string patientFullName, string doctorFullName, DateTime arrangedDateTime)
    {
        return $"Dear {patientFullName},<br>" +
               $"We are glad to inform you that your appointment with Dr. {doctorFullName} " +
               $"has been arranged for {arrangedDateTime}.<br>" +
               "Best regards,<br>" +
               "DigiDent team.";
    }
}