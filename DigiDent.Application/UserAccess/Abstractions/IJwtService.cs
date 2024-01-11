using System.Security.Claims;
using DigiDent.Application.UserAccess.Commands.Refresh;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Application.UserAccess.Tokens;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.UserAccessContext.Users;

namespace DigiDent.Application.UserAccess.Abstractions;

public interface IJwtService
{
    Task<AuthenticationResponse> GenerateAuthenticationResponseAsync(
        User user, CancellationToken cancellationToken); 
    
    Task<Result<ClaimsPrincipal>> ValidateRefreshRequestAsync(
        string accessToken, string refreshToken, CancellationToken cancellationToken);
}