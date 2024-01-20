using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
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
    
    public static Assistant Create(PersonCreationArgs args)
    {
        var assistantId = TypedId.New<EmployeeId>();
        return new Assistant(
            assistantId,
            args.Email,
            args.PhoneNumber,
            args.FullName);
    }
}