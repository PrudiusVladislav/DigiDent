namespace DigiDent.Notification.Infrastructure.EmailTemplates;

public record ActivationEmailData
    (string PatientFullName, string ActivationLink);