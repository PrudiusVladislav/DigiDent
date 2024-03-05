namespace DigiDent.Notification.Infrastructure.EmailTemplates.DataModels;

public record PatientReminderEmailData(
    string PatientName,
    string DoctorName,
    DateOnly AppointmentDate,
    TimeOnly AppointmentTime);