using DigiDent.Domain.SharedKernel.ReturnTypes;
using MediatR;

namespace DigiDent.Application.Shared.Abstractions;

/// <summary>
/// Marker interface to represent a command with a response of type <see cref="Result"/>. 
/// </summary>
public interface ICommand<TResult> : IRequest<TResult>
    where TResult : Result
{

}