using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Appointments.Commands.CreateAppointment;

public static class AppointmentCreationErrors
{
    public static Error DoctorIsNotAvailable
        => new (
            ErrorType.Validation,
            nameof(CreateAppointmentCommandHandler),
            "Doctor is not available at the given time.");
    
    public static Error NoServicesProvided
        => new (
            ErrorType.Validation,
            nameof(CreateAppointmentCommandHandler),
            "Can not create an appointment without provided services. ");
}