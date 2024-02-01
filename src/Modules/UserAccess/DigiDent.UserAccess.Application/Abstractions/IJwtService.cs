using System.Security.Claims;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.UserAccess.Application.Commands.Shared;
using DigiDent.UserAccess.Domain.Users;

namespace DigiDent.UserAccess.Application.Abstractions;

public interface IJwtService
{
    Task<AuthenticationResponse> GenerateAuthenticationResponseAsync(
        User user, CancellationToken cancellationToken); 
    
    Task<Result<ClaimsPrincipal>> ValidateRefreshRequestAsync(
        string accessToken,
        string refreshToken,
        CancellationToken cancellationToken);
    
}