using DigiDent.Notification.Application.ValueObjects;

namespace DigiDent.Notification.Application.Abstractions;

public interface IEmailContentFactory
{
    EmailContent CreateAppointmentArrangedEmail(
        string patientName, string doctorName, DateTime arrangedDateTime);
    
    EmailContent CreateActivationEmail(string patientFullName, string activationLink);
    
    EmailContent CreatePatientReminder(
        string patientName, string doctorName, DateTime appointmentDateTime);
    
    EmailContent CreateUserActivatedEmail(string fullName, string email);
}