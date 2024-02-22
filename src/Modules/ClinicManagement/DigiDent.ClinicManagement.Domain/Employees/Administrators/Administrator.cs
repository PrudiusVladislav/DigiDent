using DigiDent.ClinicManagement.Domain.Employees.Shared;
using DigiDent.ClinicManagement.Domain.Employees.Shared.Events;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.ClinicManagement.Domain.Employees.Administrators;

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
        
        Raise(new EmployeeAddedDomainEvent(
            DateTime.Now, AddedEmployee: this));
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

    protected override Result IsLegalWorkingAge(DateOnly birthDateToCheck)
    {
        const int legalWorkingAge = 18;
        return ValidateBirthDate<Administrator>(birthDateToCheck, legalWorkingAge);
    }
}