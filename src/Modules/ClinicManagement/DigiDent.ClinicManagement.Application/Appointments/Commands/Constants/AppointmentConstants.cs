namespace DigiDent.ClinicManagement.Application.Appointments.Commands.Constants;

public class AppointmentConstants
{
    // defines the time before the appointment when the reminder is sent to the patient
    public static readonly TimeSpan PatientAppointmentReminderTime = TimeSpan.FromDays(1);
}