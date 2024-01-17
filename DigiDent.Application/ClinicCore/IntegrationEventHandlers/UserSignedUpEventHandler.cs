using System.Reflection;
using DigiDent.Application.Shared.IntegrationEvents;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;
using MediatR;

namespace DigiDent.Application.ClinicCore.IntegrationEventHandlers;

public class UserSignedUpEventHandler 
    : INotificationHandler<UserSignedUpIntegrationEvent>
{
    public Task Handle(
        UserSignedUpIntegrationEvent notification,
        CancellationToken cancellationToken)
    {
        var personType = GetPersonType(notification.Role);
        var idType = GetPersonIdType(personType);
        
        typeof(PersonFactory)
            .GetMethod(nameof(PersonFactory.CreatePerson), BindingFlags.Public | BindingFlags.Static)!
            .MakeGenericMethod(personType, idType)
            .Invoke(null, [notification.FullName, notification.Email, notification.PhoneNumber]);
        
        //TODO: add to the database
    }

    private static Type GetPersonType(Role role)
    {
        
        var personTypeNamespace = $"{PersonFactory.ClinicCoreContextRootNamespace}.{role.ToString()}";
        var personType = Type.GetType(personTypeNamespace);
        
        if (personType == null || !typeof(IPerson<>).IsAssignableFrom(personType))
            throw new InvalidOperationException("Invalid role or type.");
        
        return personType;
    }
    
    private static Type GetPersonIdType(Type personType)
    {
        //or via the naming convention like Employee -> Employee[Id]
        var idType = personType
            .GetProperties()
            .FirstOrDefault(property => property.Name == nameof(IPerson<IPersonId>.Id))!
            .PropertyType;
        
        return idType;
    }
}