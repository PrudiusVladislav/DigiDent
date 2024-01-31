using DigiDent.Shared.Domain.ReturnTypes;
using MediatR;

namespace DigiDent.Shared.Application.Abstractions;

/// <summary>
/// Defines a handler for a command of type <see cref="ICommand{TResult}"/>.
/// </summary>
/// <typeparam name="TCommand"> The type of the command. </typeparam>
/// <typeparam name="TResult"> The type of the result returned from the handler. </typeparam>
public interface ICommandHandler<in TCommand, TResult> 
    : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
    where TResult : Result
{
    
}