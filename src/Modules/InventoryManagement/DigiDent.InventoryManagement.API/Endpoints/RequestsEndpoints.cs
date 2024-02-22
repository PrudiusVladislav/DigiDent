using DigiDent.InventoryManagement.Application.Requests.Commands.CreateRequest;
using DigiDent.InventoryManagement.Domain.Requests;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
using DigiDent.Shared.Infrastructure.Api;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.Kernel.ValueObjects.Pagination;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DigiDent.InventoryManagement.API.Endpoints;

public static class RequestsEndpoints
{
    public static IEndpointRouteBuilder MapRequestsEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var requestsGroup = endpoints.MapGroup("/requests");
        requestsGroup.MapPost("", CreateRequest);
        requestsGroup.MapGet("", GetAllRequests);
        return endpoints;
    }
    
    private static async Task<IResult> CreateRequest(
        [FromBody] CreateRequestParameters parameters,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        Result<CreateRequestCommand> command = 
            CreateRequestCommand.CreateFromRequest(parameters);

        if (command.IsFailure)
            return command.ProcessFailureResponse();
        
        Result<Guid> result = await sender.Send(
            command.Value!, cancellationToken);
        
        return result.Match(onSuccess: id => 
            Results.Created($"/requests/{id}", id));
    }
    
    private static async Task<IResult> GetAllRequests(
        [AsParameters] IPaginationOptions paginationOptions,
        [FromServices] IRequestsQueriesRepository repository,
        CancellationToken cancellationToken)
    {
        PaginatedResponse<RequestSummary> requests = await repository
            .GetAllAsync(paginationOptions, cancellationToken);
        
        return Results.Ok(requests);
    }
}