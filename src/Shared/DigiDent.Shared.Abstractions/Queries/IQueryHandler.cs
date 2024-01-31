using MediatR;

namespace DigiDent.Shared.Abstractions.Queries;

/// <summary>
/// Defines a handler for a query of type <see cref="IQuery{TResponse}"/>.
/// </summary>
/// <typeparam name="TQuery"> The type of the query. </typeparam>
/// <typeparam name="TResponse"> The type of the response returned from the handler. </typeparam>
public interface IQueryHandler<in TQuery, TResponse> 
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    
}