
namespace DigiDent.ClinicManagement.Application.Appointments.Commands.CloseAppointment;

public sealed record CloseAppointmentRequest(
    string Status,
    decimal Price);