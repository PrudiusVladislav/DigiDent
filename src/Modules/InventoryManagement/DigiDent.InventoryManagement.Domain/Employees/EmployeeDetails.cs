using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;

namespace DigiDent.InventoryManagement.Domain.Employees;

public record EmployeeDetails: EmployeeSummary
{
    public ICollection<RequestSummary> Requests { get; set; }
        = new List<RequestSummary>();
    
    public ICollection<ActionSummary> Actions { get; set; }
        = new List<ActionSummary>();
}