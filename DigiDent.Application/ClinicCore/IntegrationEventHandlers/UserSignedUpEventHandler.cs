using DigiDent.Application.ClinicCore.Abstractions;
using DigiDent.Application.Shared.IntegrationEvents;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using MediatR;

namespace DigiDent.Application.ClinicCore.IntegrationEventHandlers;

public class UserSignedUpEventHandler 
    : INotificationHandler<UserSignedUpIntegrationEvent>
{
    private readonly IPersonRepository _personRepository;
    private readonly IPersonFactory _personFactory;

    public UserSignedUpEventHandler(
        IPersonRepository personRepository,
        IPersonFactory personFactory)
    {
        _personRepository = personRepository;
        _personFactory = personFactory;
    }

    public async Task Handle(
        UserSignedUpIntegrationEvent notification,
        CancellationToken cancellationToken)
    {
        PersonCreationArgs personCreationArgs = new(
            notification.FullName,
            notification.Email,
            notification.PhoneNumber);
        
        (Type, Type) personTypes = _personFactory
            .GetPersonTypesFromRole(notification.Role);
        
        var person = typeof(IPersonFactory)
            .GetMethod(nameof(IPersonFactory.CreatePerson))!
            .MakeGenericMethod(personTypes.Item1, personTypes.Item2)
            .Invoke(_personFactory, [personCreationArgs]);
        
        await (Task)typeof(IPersonRepository)
            .GetMethod(nameof(IPersonRepository.AddPersonAsync))!
            .MakeGenericMethod(personTypes.Item1, personTypes.Item2)
            .Invoke(_personRepository, [person, cancellationToken])!;
    }
}