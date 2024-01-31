using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Employees.Doctors.Errors;

public static class DoctorErrors
{
    public static Error BiographyIsTooLong 
        => new(
            ErrorType.Validation,
            nameof(Doctor),
            "Biography is too long");
}