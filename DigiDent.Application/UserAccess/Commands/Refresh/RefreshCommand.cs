using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Domain.SharedKernel;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.Refresh;

public record RefreshCommand(string Token, string RefreshToken) 
    : IRequest<Result<AuthenticationResponse>>;