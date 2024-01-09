﻿using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Domain.SharedKernel;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.Refresh;

public record RefreshCommand(string AccessToken, string RefreshToken) 
    : IRequest<Result<AuthenticationResponse>>;