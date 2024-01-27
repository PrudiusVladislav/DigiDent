using DigiDent.Application.ClinicCore.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Administrators;
using DigiDent.Domain.ClinicCoreContext.Employees.Assistants;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Infrastructure.ClinicCore;

public class PersonFactory: IPersonFactory
{ 
    private readonly Dictionary<Role, (Type, Type)> _personRoleTypes;
    private readonly Dictionary<Type, Func<PersonCreationArgs, object>> _personRoleFactories;
    
    public PersonFactory()
    {
        _personRoleTypes = new Dictionary<Role, (Type, Type)>
        {
            [Role.Patient] = (typeof(Patient), typeof(PatientId)),
            [Role.Doctor] = (typeof(Doctor), typeof(EmployeeId)),
            [Role.Assistant] = (typeof(Assistant), typeof(EmployeeId)),
            [Role.Administrator] = (typeof(Administrator), typeof(EmployeeId))
        };
        
        _personRoleFactories = new Dictionary<Type, Func<PersonCreationArgs, object>>
        {
            [typeof(Patient)] = Patient.Create,
            [typeof(Doctor)] = Doctor.Create,
            [typeof(Assistant)] = Assistant.Create,
            [typeof(Administrator)] = Administrator.Create
        };
    }
    
    public (Type, Type) GetPersonTypesFromRole(Role role)
    {
        if (_personRoleTypes.TryGetValue(role, out var personType))
            return personType;
        throw new InvalidOperationException("Invalid role or type.");
    }
    
    public TPerson CreatePerson<TPerson, TId>(PersonCreationArgs args)
        where TPerson : class, IPerson<TId>
        where TId : IPersonId
    {
        if (_personRoleFactories.TryGetValue(typeof(TPerson), out var personFactory))
            return (TPerson) personFactory(args);
        throw new InvalidOperationException("Invalid person type.");
    }
}