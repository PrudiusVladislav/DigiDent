namespace DigiDent.ClinicManagement.Application.Appointments.Commands.CreateAppointment;

public sealed record CreateAppointmentRequest(
    Guid DoctorId,
    Guid PatientId,
    DateTime DateTime,
    int Duration, 
    IEnumerable<Guid> Services);