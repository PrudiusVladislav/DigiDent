using DigiDent.InventoryManagement.Domain.Actions;
using DigiDent.InventoryManagement.Domain.Requests;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.InventoryManagement.Domain.Employees;

public class Employee: IEntity<EmployeeId, Guid>
{
    public EmployeeId Id { get; init; }
    public FullName Name { get; private set; }
    
    public ICollection<Request> Requests { get; set; }
        = new List<Request>();
    
    public ICollection<InventoryAction> Actions { get; set; }
        = new List<InventoryAction>();
    
    public Employee(EmployeeId id, FullName name)
    {
        Id = id;
        Name = name;
    }
}