﻿using DigiDent.ClinicManagement.Application.Abstractions;
using DigiDent.ClinicManagement.Domain.Shared.Abstractions;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.UserAccess.IntegrationEvents;
using MediatR;

namespace DigiDent.ClinicManagement.Application.EventHandlers;

public sealed class UserActivatedIntegrationEventHandler 
    : INotificationHandler<UserActivatedIntegrationEvent>
{
    private readonly IPersonRepository _personRepository;
    private readonly IPersonFactory _personFactory;

    public UserActivatedIntegrationEventHandler(
        IPersonRepository personRepository,
        IPersonFactory personFactory)
    {
        _personRepository = personRepository;
        _personFactory = personFactory;
    }

    public async Task Handle(
        UserActivatedIntegrationEvent notification, 
        CancellationToken cancellationToken)
    {
        PersonCreationArgs personCreationArgs = new(
            notification.FullName,
            notification.Email,
            notification.PhoneNumber);
        
        (Type personType, Type idType) = _personFactory
            .GetPersonTypesFromRole(notification.Role);
        
        var person = typeof(IPersonFactory)
            .GetMethod(nameof(IPersonFactory.CreatePerson))!
            .MakeGenericMethod(personType, idType)
            .Invoke(_personFactory, [personCreationArgs]);
        
        await (Task)typeof(IPersonRepository)
            .GetMethod(nameof(IPersonRepository.AddPersonAsync))!
            .MakeGenericMethod(personType, idType)
            .Invoke(_personRepository, [person, cancellationToken])!;
    }
}