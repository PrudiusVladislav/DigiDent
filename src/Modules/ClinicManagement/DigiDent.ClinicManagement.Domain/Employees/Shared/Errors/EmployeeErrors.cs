using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Employees.Shared.Errors;

public static class EmployeeErrors
{
    public static Error EmployeeIsTooYoung(string employeeName)
        => new(
            ErrorType.Validation,
            employeeName,
            "Employee is too young");
}