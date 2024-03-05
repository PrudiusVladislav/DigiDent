namespace DigiDent.Notification.Infrastructure.EmailTemplates.DataModels;

public record CompletedActivationEmailData(
    string PatientFullName,
    string PatientEmail);