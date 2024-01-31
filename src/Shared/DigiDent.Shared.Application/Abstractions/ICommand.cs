using DigiDent.Shared.Domain.ReturnTypes;
using MediatR;

namespace DigiDent.Shared.Application.Abstractions;

/// <summary>
/// Marker interface to represent a command with a response of type <see cref="Result"/>. 
/// </summary>
public interface ICommand<out TResult> : IRequest<TResult>
    where TResult : Result
{

}