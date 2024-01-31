using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DigiDent.UserAccess.Application.Abstractions;
using DigiDent.UserAccess.Application.Tokens;
using DigiDent.Shared.Infrastructure.Auth;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.UserAccess.Application.Commands.Shared;
using DigiDent.UserAccess.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DigiDent.UserAccess.Infrastructure.Authentication;

public class JwtService: IJwtService
{
    private readonly JwtOptions _jwtOptions;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly IRefreshTokensRepository _refreshTokensRepository;
    
    public JwtService(
        IOptions<JwtOptions> jwtOptions,
        TokenValidationParameters tokenValidationParameters,
        IRefreshTokensRepository refreshTokensRepository)
    {
        _jwtOptions = jwtOptions.Value;
        _tokenValidationParameters = tokenValidationParameters;
        _refreshTokensRepository = refreshTokensRepository;
    }
    
    public async Task<AuthenticationResponse> GenerateAuthenticationResponseAsync(
        User user, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new (CustomClaims.NameIdentifier, user.Id.Value.ToString()),
            new (CustomClaims.Jti, Guid.NewGuid().ToString()),
            new (CustomClaims.Email, user.Email.Value),
            new (CustomClaims.Role, user.Role.ToString())
        };
        
        SigningCredentials signingCredentials = new(
            _jwtOptions.SigningKey, 
            SecurityAlgorithms.HmacSha256);
        
        JwtSecurityToken token = new(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: DateTime.UtcNow.Add(_jwtOptions.TokenLifetime),
            signingCredentials: signingCredentials);
        
        string serializedToken = new JwtSecurityTokenHandler()
            .WriteToken(token);
        
        await _refreshTokensRepository.DeleteRefreshTokenByUserIdAsync(
            user.Id, cancellationToken);
        
        RefreshToken refreshToken = new(
            Token: Guid.NewGuid().ToString(),
            JwtId: token.Id,
            CreationDate: DateTime.UtcNow,
            ExpiryDate: DateTime.UtcNow.Add(_jwtOptions.RefreshTokenLifetime),
            UserId: user.Id);
        
        await _refreshTokensRepository
            .AddRefreshTokenAsync(refreshToken, cancellationToken);
        
        return new AuthenticationResponse(
            AccessToken: serializedToken, 
            RefreshToken: refreshToken.Token);
    }

    public async Task<Result<ClaimsPrincipal>> ValidateRefreshRequestAsync(
        string accessToken,
        string refreshToken,
        CancellationToken cancellationToken)
    {
        ClaimsPrincipal? tokenClaimsPrincipal = GetPrincipalFromExpiredToken(accessToken);

        if (tokenClaimsPrincipal is null)
            return Result.Fail<ClaimsPrincipal>(TokensErrors
                .InvalidToken);
        
        var expiryDateUnix = long.Parse(tokenClaimsPrincipal.Claims
                .Single(x => x.Type == CustomClaims.Exp)
                .Value);
        
        var expiryDateTimeUtc = DateTime.UnixEpoch.AddSeconds(expiryDateUnix);
        
        if (expiryDateTimeUtc > DateTime.UtcNow)
            return Result.Fail<ClaimsPrincipal>(TokensErrors
                .TokenIsNotExpired);
        
        string jti = tokenClaimsPrincipal.Claims
            .Single(x => x.Type == CustomClaims.Jti)
            .Value;
        
        RefreshToken? storedRefreshToken = await _refreshTokensRepository
            .GetRefreshTokenAsync(refreshToken, cancellationToken);

        if (storedRefreshToken is null ||
            DateTime.UtcNow > storedRefreshToken.ExpiryDate ||
            storedRefreshToken.JwtId != jti)
        {
            return Result.Fail<ClaimsPrincipal>(TokensErrors
                .InvalidToken);
        }
        
        return Result.Ok(tokenClaimsPrincipal);
    }
    
    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        try
        {
            var originalValidator = _tokenValidationParameters.LifetimeValidator;
            _tokenValidationParameters.LifetimeValidator = (
                notBefore,
                expires,
                securityToken,
                validationParameters) => true;
            
            ClaimsPrincipal tokenClaimsPrincipal = tokenHandler.ValidateToken(
                token,
                _tokenValidationParameters,
                out var validatedToken);
            
            _tokenValidationParameters.LifetimeValidator = originalValidator;
            
            if (!JwtHasValidSecurityAlgorithm(validatedToken))
                return null;
            
            return tokenClaimsPrincipal;
        }
        catch
        {
            return null;
        }
    }
    
    private static bool JwtHasValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
               jwtSecurityToken.Header.Alg.Equals(
                   SecurityAlgorithms.HmacSha256,
                   StringComparison.InvariantCultureIgnoreCase);
    }
}