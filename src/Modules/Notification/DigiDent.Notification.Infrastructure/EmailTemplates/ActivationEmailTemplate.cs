namespace DigiDent.Notification.Infrastructure.EmailTemplates;

public class ActivationEmailTemplate
{
    public static string Create(string patientFullName, string activationLink)
    { 
        return $@"
            <p>Dear {patientFullName},</p>
            <p>Thank you for registering with DigiDent. To activate your account, please click the link below:</p>
            <p><a href=""{activationLink}"">Activate account</a></p>
            <p>Best regards,</p>
            <p>DigiDent team.</p>
        ";
    }
}