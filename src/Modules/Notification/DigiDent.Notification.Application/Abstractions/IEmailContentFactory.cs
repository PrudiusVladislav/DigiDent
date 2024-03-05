using DigiDent.Notification.Application.ValueObjects;

namespace DigiDent.Notification.Application.Abstractions;

public interface IEmailContentFactory
{
    Task<EmailContent> CreateAppointmentArrangedEmail(
        string patientName, string doctorName, DateTime arrangedDateTime);
    
    Task<EmailContent> CreateActivationEmail(
        string patientFullName, string activationLink);
    
    Task<EmailContent> CreatePatientReminder(
        string patientName, string doctorName, DateTime appointmentDateTime);
    
    Task<EmailContent> CreateUserActivatedEmail(
        string fullName, string email);
}