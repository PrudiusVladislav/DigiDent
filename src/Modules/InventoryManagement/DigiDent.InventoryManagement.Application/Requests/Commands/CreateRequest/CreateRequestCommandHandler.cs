using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Requests;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.InventoryManagement.Application.Requests.Commands.CreateRequest;

public sealed class CreateRequestCommandHandler
    : ICommandHandler<CreateRequestCommand, Result<Guid>>
{
    private readonly IRequestsRepository _requestsRepository;
    private readonly IInventoryItemsRepository _itemsRepository;
    private readonly IEmployeesRepository _employeesRepository;
    
    public CreateRequestCommandHandler(
        IRequestsRepository requestsRepository, 
        IInventoryItemsRepository itemsRepository, 
        IEmployeesRepository employeesRepository)
    {
        _requestsRepository = requestsRepository;
        _itemsRepository = itemsRepository;
        _employeesRepository = employeesRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateRequestCommand command, CancellationToken cancellationToken)
    {
        InventoryItem? item = await _itemsRepository.GetByIdAsync(
            command.RequestedItemId, cancellationToken);
        if (item is null)
        {
            return Result.Fail<Guid>(RepositoryErrors.
                EntityNotFound<InventoryItem, int>(command.RequestedItemId));
        }
        
        Employee? employee = await _employeesRepository.GetByIdAsync(
            command.RequesterId, cancellationToken);
        
        if (employee is null)
        {
            return Result.Fail<Guid>(RepositoryErrors.
                EntityNotFound<Employee, Guid>(command.RequesterId));
        }
        
        Request request = new(
            command.RequestedItemId,
            command.RequestedQuantity,
            command.RequesterId,
            command.Remarks);
        
        await _requestsRepository.AddAsync(request, cancellationToken);
        return Result.Ok(request.Id.Value);
    }
}