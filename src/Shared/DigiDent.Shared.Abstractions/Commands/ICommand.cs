using DigiDent.Shared.Kernel.ReturnTypes;
using MediatR;

namespace DigiDent.Shared.Abstractions.Commands;

/// <summary>
/// Marker interface to represent a command with a response of type <see cref="Result"/>. 
/// </summary>
public interface ICommand<out TResult> : IRequest<TResult>
    where TResult : Result
{

}