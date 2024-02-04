namespace DigiDent.ClinicManagement.IntegrationEvents;

public class SendPatientReminderForAppointment
{
    public string PatientEmail { get; set; } = string.Empty;
    public string PatientFullName { get; set; } = string.Empty;
    public DateTime ArrangedDateTime { get; set; }
    public string DoctorFullName { get; set; } = string.Empty;
}