using DigiDent.Shared.Domain.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Doctors.Errors;

public static class DoctorErrors
{
    public static Error BiographyIsTooLong 
        => new(
            ErrorType.Validation,
            nameof(Doctor),
            "Biography is too long");
}