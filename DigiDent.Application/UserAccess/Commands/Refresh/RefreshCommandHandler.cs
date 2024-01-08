using System.Security.Claims;
using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Application.UserAccess.Tokens;
using DigiDent.Domain.SharedKernel;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.Refresh;

public class RefreshCommandHandler
    : IRequestHandler<RefreshCommand, Result<AuthenticationResponse>>
{
    private readonly IJwtService _jwtService;
    
    public RefreshCommandHandler(
        IJwtService jwtService)
    {
        _jwtService = jwtService;
    }
    
    public async Task<Result<AuthenticationResponse>> Handle(
        RefreshCommand request,
        CancellationToken cancellationToken)
    {
        //TODO: Move implementation from JwtService to here
        var refreshTokenResult = await _jwtService
            .RefreshTokenAsync(request, cancellationToken);
        return refreshTokenResult;
    }
}