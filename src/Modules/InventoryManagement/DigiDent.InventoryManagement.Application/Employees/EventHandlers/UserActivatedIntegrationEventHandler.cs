using DigiDent.ClinicManagement.IntegrationEvents;
using DigiDent.InventoryManagement.Domain.Employees;
using MediatR;

namespace DigiDent.InventoryManagement.Application.Employees.EventHandlers;

public sealed class UserActivatedIntegrationEventHandler
    : INotificationHandler<EmployeeAddedIntegrationEvent>
{
    private readonly IEmployeesRepository _employeesRepository;

    public UserActivatedIntegrationEventHandler(
        IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public async Task Handle(
        EmployeeAddedIntegrationEvent notification, 
        CancellationToken cancellationToken)
    {
        EmployeeId employeeId = new(notification.Id);
        Employee employee = new(employeeId, notification.FullName);
        
        await _employeesRepository.AddAsync(employee, cancellationToken);
    }
}