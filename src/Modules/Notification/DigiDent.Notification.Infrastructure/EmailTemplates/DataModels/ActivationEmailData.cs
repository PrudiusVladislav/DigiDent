namespace DigiDent.Notification.Infrastructure.EmailTemplates.DataModels;

public record ActivationEmailData
    (string PatientFullName, string ActivationLink);