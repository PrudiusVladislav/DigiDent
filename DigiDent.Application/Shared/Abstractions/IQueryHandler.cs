using MediatR;

namespace DigiDent.Application.Shared.Abstractions;

public interface IQueryHandler<in TQuery, TResult> 
    : IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    
}