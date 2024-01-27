
namespace DigiDent.Application.ClinicCore.Appointments.Commands.CloseAppointment;

public sealed record CloseAppointmentRequest(
    string Status,
    decimal Price);