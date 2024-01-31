using DigiDent.Shared.Domain.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Shared.Errors;

public static class EmployeeErrors
{
    public static Error EmployeeIsTooYoung(string employeeName)
        => new(
            ErrorType.Validation,
            employeeName,
            "Employee is too young");
}