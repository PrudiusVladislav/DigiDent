using MediatR;

namespace DigiDent.Shared.Abstractions.Queries;

/// <summary>
/// Marker interface to represent a query.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface IQuery<out TResponse>: IRequest<TResponse>
{
    
}