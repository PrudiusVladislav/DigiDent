using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Shared.Errors;

public static class EmployeeErrors
{
    public static Error EmployeeIsTooYoung(string employeeName)
        => new(
            ErrorType.Validation,
            employeeName,
            "Employee is too young");
}