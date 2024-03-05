using DigiDent.Notification.Application.Abstractions;
using DigiDent.Notification.Application.ValueObjects;
using DigiDent.Notification.Infrastructure.EmailTemplates;
using DigiDent.Notification.Infrastructure.EmailTemplates.DataModels;
using DigiDent.Shared.Kernel.Extensions;
using Razor.Templating.Core;

namespace DigiDent.Notification.Infrastructure.Services;

public class EmailContentFactory: IEmailContentFactory
{
    public async Task<EmailContent> CreateAppointmentArrangedEmail(
        string patientName, string doctorName, DateTime arrangedDateTime)
    {
        AppointmentArrangedEmailData dataModel = new(
            patientName,
            doctorName,
            arrangedDateTime.ToDateOnly(),
            arrangedDateTime.ToTimeOnly());
        
        return new EmailContent(
            Subject: "Appointment arranged",
            HtmlBody: await RazorTemplateEngine.RenderAsync(
                "~/EmailTemplates/AppointmentArrangedEmail.cshtml", dataModel));
    }

    public async Task<EmailContent> CreateActivationEmail(
        string patientFullName, string activationLink)
    {
        ActivationEmailData dataModel = new(patientFullName, activationLink);
        return new EmailContent(
            Subject: "Activate your account",
            HtmlBody: await RazorTemplateEngine.RenderAsync(
                 "~/EmailTemplates/ActivationEmail.cshtml", dataModel));
    }
    
    public async Task<EmailContent> CreatePatientReminder(
        string patientName, string doctorName , DateTime appointmentDateTime)
    {
        PatientReminderEmailData dataModel = new(
            patientName,
            doctorName,
            appointmentDateTime.ToDateOnly(),
            appointmentDateTime.ToTimeOnly());
        
        return new EmailContent(
            Subject: "Appointment reminder",
            HtmlBody: await RazorTemplateEngine.RenderAsync(
                "~/EmailTemplates/PatientReminderEmail.cshtml", dataModel));
    }
    
    public async Task<EmailContent> CreateUserActivatedEmail(
        string fullName, string email)
    {
        CompletedActivationEmailData dataModel = new(fullName, email);
        return new EmailContent(
            Subject: "Account activated",
            HtmlBody: await RazorTemplateEngine.RenderAsync(
                "~/EmailTemplates/CompletedActivationEmail.cshtml", dataModel));
    }
}