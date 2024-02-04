namespace DigiDent.Notification.Infrastructure.EmailTemplates;

public class PatientReminderEmailTemplate
{
    public static string Create(string patientName, string doctorName, DateTime appointmentDateTime)
    {
        return $@"
            <html>
                <body>
                    <h1>Appointment reminder</h1>
                    <p>Dear {patientName},</p>
                    <p>This is a reminder for your appointment with Dr. {doctorName} on {appointmentDateTime}.</p>
                    <p>Thank you.</p>
                </body>
            </html>";
    }
}