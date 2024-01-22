namespace DigiDent.Application.ClinicCore.Appointments.Commands.CreateAppointment;

public sealed record CreateAppointmentRequest(
    Guid DoctorId,
    Guid PatientId,
    DateTime Date,
    TimeSpan Duration, 
    IEnumerable<Guid> Services);