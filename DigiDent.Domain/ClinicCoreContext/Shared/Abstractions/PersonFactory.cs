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
        return (TPerson)Activator.CreateInstance(typeof(TPerson), personId, fullName, email, phoneNumber)!;
    }
    
    public static string ClinicCoreContextRootNamespace
        => typeof(DependencyInjection).Namespace!;
    
}