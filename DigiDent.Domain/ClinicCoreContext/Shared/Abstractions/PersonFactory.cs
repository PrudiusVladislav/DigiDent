using System.Reflection;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

public static class PersonFactory
{ 
    public static TPerson CreatePerson<TPerson, TId>(
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber)
        where TPerson : IPerson<TId>
        where TId : IPersonId
    {
        var personId = TypedId.New<TId>();
        
        BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
        object[] parameters = [personId, fullName, email, phoneNumber];
        
        return (TPerson)Activator.CreateInstance(
            typeof(TPerson), flags,null, parameters, null)!;
    }
}