using DigiDent.Domain.SharedKernel.ReturnTypes;
using MediatR;

namespace DigiDent.Application.Shared.Abstractions;

public interface ICommand<TResult> : IRequest<TResult>
    where TResult : Result
{

}