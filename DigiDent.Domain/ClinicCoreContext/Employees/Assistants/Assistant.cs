using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Assistants;

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