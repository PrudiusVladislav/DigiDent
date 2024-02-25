using DigiDent.ClinicManagement.IntegrationEvents;
using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Employees.Repositories;
using DigiDent.InventoryManagement.Domain.Employees.ValueObjects;
using DigiDent.Shared.Kernel.ValueObjects;
using MediatR;

namespace DigiDent.InventoryManagement.Application.Employees.EventHandlers;

public sealed class EmployeeAddedIntegrationEventHandler
    : INotificationHandler<EmployeeAddedIntegrationEvent>
{
    private readonly IEmployeesCommandsRepository _employeesCommandsRepository;

    public EmployeeAddedIntegrationEventHandler(
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
            GetPositionFromRole(notification.Role));
        
        await _employeesCommandsRepository.AddAsync(employee, cancellationToken);
    }

    private static Position GetPositionFromRole(Role role)
    {
        return role switch
        {
            Role.Doctor => Position.Doctor,
            Role.Administrator => Position.Administrator,
            Role.Assistant => Position.Assistant,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }
}