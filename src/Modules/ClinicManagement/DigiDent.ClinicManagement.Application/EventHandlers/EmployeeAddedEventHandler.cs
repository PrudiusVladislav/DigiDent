using DigiDent.ClinicManagement.Domain.Employees.Shared.Events;
using DigiDent.ClinicManagement.IntegrationEvents;
using DigiDent.Shared.Kernel.Extensions;
using DigiDent.Shared.Kernel.ValueObjects;
using MediatR;

namespace DigiDent.ClinicManagement.Application.EventHandlers;

public sealed class EmployeeAddedEventHandler
    : INotificationHandler<EmployeeAddedDomainEvent>
{
    private readonly IPublisher _publisher;

    public EmployeeAddedEventHandler(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Handle(
        EmployeeAddedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        EmployeeAddedIntegrationEvent integrationEvent = new()
        {
            TimeOfOccurrence = notification.TimeOfOccurrence,
            Id = notification.AddedEmployee.Id.Value,
            FullName = notification.AddedEmployee.FullName,
            Email = notification.AddedEmployee.Email,
            PhoneNumber = notification.AddedEmployee.PhoneNumber,
            Role = notification.AddedEmployee.GetType().Name.ToEnum<Role>().Value
        };
        
        await _publisher.Publish(integrationEvent, cancellationToken);
    }
}