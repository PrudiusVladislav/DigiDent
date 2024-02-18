using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.InventoryManagement.Domain.Requests.Errors;
using DigiDent.InventoryManagement.Domain.Requests.Events;
using DigiDent.InventoryManagement.Domain.Requests.ValueObjects;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.Extensions;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.InventoryManagement.Domain.Requests;

public class Request : 
    AggregateRoot, 
    IEntity<RequestId, Guid>
{
    public RequestId Id { get; init; }
    public RequestStatus Status { get; private set; }
    
    public InventoryItemId RequestedItemId { get; private set; }
    public InventoryItem RequestedItem { get; private set; } = null!;
    public Quantity RequestedQuantity { get; private set; }
    
    public EmployeeId RequesterId { get; init; }
    public Employee Requester { get; init; }
    
    public DateOnly DateOfRequest { get; init; }
    public string Remarks { get; set; }
    
    public Request(
        InventoryItemId requestedItemId,
        Quantity requestedQuantity,
        Employee requester,
        string remarks = "")
    {
        Id = TypedId.New<RequestId>();
        Status = RequestStatus.DecisionPending;
        RequestedItemId = requestedItemId;
        RequestedQuantity = requestedQuantity;
        Requester = requester;
        Remarks = remarks;
        DateOfRequest = DateTime.Now.ToDateOnly();
        
        //TODO: Consider raising domain event for request creation
    }

    public Result ApproveRequest()
    {
        return SetDecisionStatus(
            RequestStatus.Processing, nameof(ApproveRequest));
    }

    public Result RejectRequest()
    {
        return SetDecisionStatus(
            RequestStatus.Rejected, nameof(RejectRequest));
    }
    
    public Result CompleteRequest()
    {
        if (Status != RequestStatus.Processing)
        {
            return Result.Fail(RequestStatusErrors
                .RequestNotInProcessing);
        }
        
        Status = RequestStatus.Completed;
        Raise(new RequestCompletedDomainEvent(
            TimeOfOccurrence: DateTime.Now,
            Request: this));
        
        return Result.Ok();
    }
    
    private Result SetDecisionStatus(
        RequestStatus status, string methodName)
    {
        if (Status != RequestStatus.DecisionPending)
        {
            return Result.Fail(RequestStatusErrors
                .DecisionAlreadyMade(methodName));
        }

        Status = status;
        return Result.Ok();
    }
}