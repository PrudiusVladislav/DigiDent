using DigiDent.Notification.Application.Abstractions;
using DigiDent.Notification.Application.ValueObjects;
using DigiDent.Notification.Infrastructure.EmailTemplates;

namespace DigiDent.Notification.Infrastructure.Services;

public class EmailContentFactory: IEmailContentFactory
{
    public EmailContent CreateAppointmentArrangedEmail(
        string patientName, string doctorName, DateTime arrangedDateTime)
    {
        return new EmailContent(
            Subject: "Appointment arranged",
            HtmlBody: AppointmentArrangedEmailTemplate.Create(
                patientName, doctorName, arrangedDateTime));
    }

    public EmailContent CreateActivationEmail(string message, string activationLink)
    {
        return new EmailContent(
            Subject: "Activate your account",
            HtmlBody: ActivationEmailTemplate.Create(message, activationLink));
    }
    
    public EmailContent CreatePatientReminder(string patientName, string doctorName , DateTime appointmentDateTime)
    {
        return new EmailContent(
            Subject: "Appointment reminder",
            HtmlBody: PatientReminderEmailTemplate.Create(patientName, doctorName, appointmentDateTime));
    }
    
    public EmailContent CreateUserActivatedEmail(string fullName, string email)
    {
        return new EmailContent(
            Subject: "Account activated",
            HtmlBody: UserActivatedEmailTemplate.Create(fullName, email));
    }
}