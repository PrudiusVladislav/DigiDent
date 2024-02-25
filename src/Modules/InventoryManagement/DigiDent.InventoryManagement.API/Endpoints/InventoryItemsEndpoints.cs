using DigiDent.InventoryManagement.Application.Items.Commands.AddNewItem;
using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.Repositories;
using DigiDent.Shared.Infrastructure.Api;
using DigiDent.Shared.Infrastructure.Pagination;
using DigiDent.Shared.Kernel.Pagination;
using DigiDent.Shared.Kernel.ReturnTypes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DigiDent.InventoryManagement.API.Endpoints;

public static class InventoryItemsEndpoints
{
    internal static IEndpointRouteBuilder MapInventoryItemsEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var itemsGroup = endpoints.MapGroup("/items");
        
        itemsGroup.MapGet("", GetAllItems);
        itemsGroup.MapPost("", AddNewItem);
        return endpoints;
    }

    private static async Task<IResult> GetAllItems(
        [AsParameters] PaginationDTO pagination,
        [FromServices] IInventoryItemsQueriesRepository commandsRepository,
        CancellationToken cancellationToken)
    {
        PaginatedResponse<InventoryItemSummary> response = await commandsRepository
            .GetAllAsync(pagination, cancellationToken);
        
        return Results.Ok(response);
    }
    
    private static async Task<IResult> AddNewItem(
        [FromBody] AddNewItemRequest request,
        [FromServices]ISender sender,
        CancellationToken cancellationToken)
    {
        Result<AddNewItemCommand> command = AddNewItemCommand
            .CreateFromRequest(request);

        if (command.IsFailure)
            return command.ProcessFailureResponse();
        
        Result<int> result = await sender.Send(
            command.Value!, cancellationToken);
        
        return result.Match(
            onSuccess: id => Results.Created($"/items/{id}", id));
    }
}