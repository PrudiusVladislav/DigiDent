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
    private readonly IRequestsCommandsRepository _requestsCommandsRepository;
    private readonly IInventoryItemsCommandsRepository _itemsCommandsRepository;
    private readonly IEmployeesRepository _employeesRepository;
    
    public CreateRequestCommandHandler(
        IRequestsCommandsRepository requestsCommandsRepository, 
        IInventoryItemsCommandsRepository itemsCommandsRepository, 
        IEmployeesRepository employeesRepository)
    {
        _requestsCommandsRepository = requestsCommandsRepository;
        _itemsCommandsRepository = itemsCommandsRepository;
        _employeesRepository = employeesRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateRequestCommand command, CancellationToken cancellationToken)
    {
        InventoryItem? item = await _itemsCommandsRepository.GetByIdAsync(
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
        
        await _requestsCommandsRepository.AddAsync(request, cancellationToken);
        return Result.Ok(request.Id.Value);
    }
}