using DigiDent.ClinicManagement.Domain.Employees.Shared;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.ClinicManagement.Domain.Employees.Assistants;

public class Assistant : Employee
{
    internal Assistant(
        EmployeeId id,
        Email email,
        PhoneNumber phoneNumber,
        FullName fullName)
    {
        Id = id;
        Email = email;
        PhoneNumber = phoneNumber;
        FullName = fullName;
    }
    
    public static Assistant Create(PersonCreationArgs args)
    {
        var assistantId = TypedId.New<EmployeeId>();
        return new Assistant(
            assistantId,
            args.Email,
            args.PhoneNumber,
            args.FullName);
    }

    protected override Result IsLegalWorkingAge(DateOnly birthDateToCheck)
    { 
        const int legalWorkingAge = 18;
        return ValidateBirthDate<Assistant>(birthDateToCheck, legalWorkingAge);
    }
}