using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.SharedKernel.Abstractions;
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
    
    public static Assistant Create(
        Email email,
        PhoneNumber phoneNumber,
        FullName fullName)
    {
        var assistantId = TypedId.New<EmployeeId>();
        return new Assistant(
            assistantId,
            email,
            phoneNumber,
            fullName);
    }
        
}