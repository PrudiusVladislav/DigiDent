using DigiDent.ClinicManagement.IntegrationEvents;
using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Employees.Repositories;
using MediatR;

namespace DigiDent.InventoryManagement.Application.Employees.EventHandlers;

public sealed class UserActivatedIntegrationEventHandler
    : INotificationHandler<EmployeeAddedIntegrationEvent>
{
    private readonly IEmployeesCommandsRepository _employeesCommandsRepository;

    public UserActivatedIntegrationEventHandler(
        IEmployeesCommandsRepository employeesCommandsRepository)
    {
        _employeesCommandsRepository = employeesCommandsRepository;
    }

    public async Task Handle(
        EmployeeAddedIntegrationEvent notification, 
        CancellationToken cancellationToken)
    {
        EmployeeId employeeId = new(notification.Id);
        Employee employee = new(
            employeeId, 
            notification.FullName,
            notification.Email,
            notification.PhoneNumber,
            notification.Role);
        
        await _employeesCommandsRepository.AddAsync(employee, cancellationToken);
    }
}