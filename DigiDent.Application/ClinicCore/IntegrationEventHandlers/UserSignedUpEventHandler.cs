using System.Reflection;
using DigiDent.Application.Shared.IntegrationEvents;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;
using MediatR;

namespace DigiDent.Application.ClinicCore.IntegrationEventHandlers;

public class UserSignedUpEventHandler 
    : INotificationHandler<UserSignedUpIntegrationEvent>
{
    private readonly IPersonRepository _personRepository;

    public UserSignedUpEventHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task Handle(
        UserSignedUpIntegrationEvent notification,
        CancellationToken cancellationToken)
    {
        var personType = GetPersonType(notification.Role);
        var idType = GetPersonIdType(personType);
        
        var person = typeof(PersonFactory)
            .GetMethod(nameof(PersonFactory.CreatePerson), BindingFlags.Public | BindingFlags.Static)!
            .MakeGenericMethod(personType, idType)
            .Invoke(null, [notification.FullName, notification.Email, notification.PhoneNumber]);
        
        await (Task)typeof(IPersonRepository)
            .GetMethod(nameof(IPersonRepository.AddAsync), BindingFlags.Public | BindingFlags.Instance)!
            .MakeGenericMethod(personType, idType)
            .Invoke(_personRepository, [person, cancellationToken])!;
    }

    private static Type GetPersonType(Role role)
    {
        var personType = Assembly
            .GetAssembly(typeof(IPerson<>))!
            .GetTypes()
            .SingleOrDefault(type =>
                type is { IsClass: true, IsAbstract: false } &&
                type.GetInterfaces()
                    .Any(i => 
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IPerson<>)) &&
                type.Name.Equals(role.ToString()));
        
        if (personType == null)
            throw new InvalidOperationException("Invalid role or type.");
        
        return personType;
    }
    
    private static Type GetPersonIdType(Type personType)
    {
        var idType = personType
            .GetProperties()
            .SingleOrDefault(property => 
                property.Name == nameof(IPerson<IPersonId>.Id))!
            .PropertyType;
        
        return idType;
    }
}