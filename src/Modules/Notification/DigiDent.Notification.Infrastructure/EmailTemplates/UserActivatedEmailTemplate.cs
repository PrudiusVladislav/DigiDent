namespace DigiDent.Notification.Infrastructure.EmailTemplates;

public class UserActivatedEmailTemplate
{
    public static string Create(string fullName, string email)
    {
        return $@"
            <h1>Account activated</h1>
            <p>
                Dear {fullName},
            </p>
            <p>
                Your account has been activated. You can now log in using your email address: {email}.
            </p>
        ";
    }
}