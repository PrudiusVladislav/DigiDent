using DigiDent.InventoryManagement.Application.Actions.Commands.MakeInventoryAction;
using DigiDent.InventoryManagement.Domain.Actions;
using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.Shared.Infrastructure.Api;
using DigiDent.Shared.Infrastructure.Pagination;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.Kernel.ValueObjects.Pagination;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DigiDent.InventoryManagement.API.Endpoints;

public static class InventoryActionsEndpoints
{
    internal static IEndpointRouteBuilder MapInventoryActionsEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var actionsGroup = endpoints.MapGroup("/actions");
        
        actionsGroup.MapPost("", MakeInventoryAction);
        actionsGroup.MapGet("", GetAllActions);
        
        return endpoints;
    }

    private static async Task<IResult> MakeInventoryAction(
        [FromBody]MakeInventoryActionRequest request,
        [FromServices]ISender sender,
        CancellationToken cancellationToken)
    {
        Result<MakeInventoryActionCommand> command = MakeInventoryActionCommand
            .CreateFromRequest(request);

        if (command.IsFailure)
            return command.ProcessFailureResponse();
        
        Result<int> response = await sender.Send(command.Value!, cancellationToken);
        
        return response.Match(
            onSuccess: id => Results.Created($"/actions/{id}", id));
    }
    
    private static async Task<IResult> GetAllActions(
        [AsParameters] PaginationDTO pagination,
        [FromServices] IInventoryActionsQueriesRepository commandsRepository,
        CancellationToken cancellationToken)
    {
        PaginatedResponse<ActionSummary> response = await commandsRepository
            .GetAllAsync(pagination, cancellationToken);
        
        return Results.Ok(response);
    }
}