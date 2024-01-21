using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Administrators;

public class Administrator: Employee
{
    internal Administrator(
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
    
    public static Administrator Create(PersonCreationArgs args)
    {
        var administratorId = TypedId.New<EmployeeId>();
        return new Administrator(
            administratorId,
            args.Email,
            args.PhoneNumber,
            args.FullName);
    }
    
    public override Result IsLegalWorkingAge(DateOnly birthDateToCheck)
    {
        const int legalWorkingAge = 18;
        return ValidateBirthDate<Administrator>(birthDateToCheck, legalWorkingAge);
    }
}