using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Appointments.Commands.CreateAppointment;

public static class AppointmentCreationErrors
{
    public static Error AppointmentDateIsInThePast
        => new (
            ErrorType.Validation,
            nameof(CreateAppointmentCommandHandler),
            "Date of a new appointment must be in the future");
    
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