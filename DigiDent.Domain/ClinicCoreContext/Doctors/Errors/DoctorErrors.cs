using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Doctors.Errors;

public static class DoctorErrors
{
    public static Error BiographyIsTooLong 
        => new(
            ErrorType.Validation,
            nameof(Doctor),
            "Biography is too long");
}