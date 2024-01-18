using MediatR;

namespace DigiDent.Application.Shared.Abstractions;

public interface IQuery<TResult>: IRequest<TResult>
{
    
}