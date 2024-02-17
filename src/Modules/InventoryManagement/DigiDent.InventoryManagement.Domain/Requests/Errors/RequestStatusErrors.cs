using DigiDent.InventoryManagement.Domain.Requests.ValueObjects;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.InventoryManagement.Domain.Requests.Errors;

public static class RequestStatusErrors
{
    public static Error DecisionAlreadyMade(string methodName) => 
        new (
            ErrorType.Validation,
            Origin: nameof(Request),
            Message: $"To perform '{methodName}', the request status" + 
            $" must be '{RequestStatus.DecisionPending.ToString()}'");
    
    public static Error RequestNotInProcessing => 
        new (
            ErrorType.Validation,
            Origin: nameof(Request),
            Message: "To complete the request, the request status" + 
            $" must be set to '{RequestStatus.Processing.ToString()}'");
}