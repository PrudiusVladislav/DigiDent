﻿using DigiDent.Domain.SharedKernel.ReturnTypes;
using MediatR;

namespace DigiDent.Application.Shared.Abstractions;

public interface ICommandHandler<in TCommand, TResult> 
    : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
    where TResult : Result
{
    
}