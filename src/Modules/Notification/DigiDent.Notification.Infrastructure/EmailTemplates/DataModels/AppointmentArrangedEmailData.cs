namespace DigiDent.Notification.Infrastructure.EmailTemplates.DataModels;

public record AppointmentArrangedEmailData(
    string PatientName,
    string DoctorName,
    DateOnly AppointmentDate,
    TimeOnly AppointmentTime);