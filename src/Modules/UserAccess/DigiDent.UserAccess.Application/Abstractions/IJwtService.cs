using System.Security.Claims;
using DigiDent.Application.UserAccess.Commands.Refresh;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Application.UserAccess.Tokens;
using DigiDent.Shared.Domain.Abstractions;
using DigiDent.Shared.Domain.ReturnTypes;
using DigiDent.UserAccess.Domain.Users;

namespace DigiDent.Application.UserAccess.Abstractions;

public interface IJwtService
{
    Task<AuthenticationResponse> GenerateAuthenticationResponseAsync(
        User user, CancellationToken cancellationToken); 
    
    Task<Result<ClaimsPrincipal>> ValidateRefreshRequestAsync(
        string accessToken,
        string refreshToken,
        CancellationToken cancellationToken);
    
}