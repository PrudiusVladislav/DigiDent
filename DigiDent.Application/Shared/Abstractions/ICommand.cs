using MediatR;

namespace DigiDent.Application.Shared.Abstractions;

public interface ICommand<TResult> : IRequest<TResult>
{

}