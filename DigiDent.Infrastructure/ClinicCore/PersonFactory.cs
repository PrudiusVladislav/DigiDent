using DigiDent.Application.ClinicCore.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Assistants;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Infrastructure.ClinicCore;

public class PersonFactory: IPersonFactory
{ 
    private readonly Dictionary<Role, Type> _personRoleTypes;
    private readonly Dictionary<Type, Func<PersonCreationArgs, object>> _personRoleFactories;
    
    public PersonFactory()
    {
        _personRoleTypes = new Dictionary<Role, Type>
        {
            [Role.Patient] = typeof(Patient),
            [Role.Doctor] = typeof(Doctor),
            [Role.Assistant] = typeof(Assistant)
        };
        
        _personRoleFactories = new Dictionary<Type, Func<PersonCreationArgs, object>>
        {
            [typeof(Patient)] = Patient.Create,
            [typeof(Doctor)] = Doctor.Create,
            [typeof(Assistant)] = Assistant.Create
        };
    }
    
    public Type GetPersonTypeFromRole(Role role)
    {
        if (_personRoleTypes.TryGetValue(role, out var personType))
            return personType;
        throw new InvalidOperationException("Invalid role or type.");
    }
    
    public TPerson CreatePerson<TPerson>(PersonCreationArgs args)
        where TPerson : class, IPerson<IPersonId>
    {
        if (_personRoleFactories.TryGetValue(typeof(TPerson), out var personFactory))
            return (TPerson) personFactory(args);
        throw new InvalidOperationException("Invalid person type.");
    }
}