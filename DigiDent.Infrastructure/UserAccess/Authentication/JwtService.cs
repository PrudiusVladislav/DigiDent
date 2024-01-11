using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Application.UserAccess.Tokens;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.UserAccessContext.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DigiDent.Infrastructure.UserAccess.Authentication;

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
        User user,
        CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new (CustomClaims.NameIdentifier, user.Id.Value.ToString()),
            new (CustomClaims.Jti, Guid.NewGuid().ToString()),
            new (CustomClaims.Email, user.Email.Value),
            new (CustomClaims.Role, user.Role.ToString())
        };
        
        var signingCredentials = new SigningCredentials(
            _jwtOptions.SigningKey,
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: DateTime.UtcNow.Add(_jwtOptions.TokenLifetime),
            signingCredentials: signingCredentials);
        string serializedToken = new JwtSecurityTokenHandler()
            .WriteToken(token);
        
        await _refreshTokensRepository.DeleteRefreshTokenByUserIdAsync(
            user.Id, cancellationToken);
        
        var refreshToken = new RefreshToken()
        {
            Token = Guid.NewGuid().ToString(),
            JwtId = token.Id,
            UserId = user.Id,
            CreationDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.Add(_jwtOptions.RefreshTokenLifetime)
        };
        
        await _refreshTokensRepository
            .AddRefreshTokenAsync(refreshToken, cancellationToken);
        
        return new AuthenticationResponse(serializedToken, refreshToken.Token);
    }

    public async Task<Result<ClaimsPrincipal>> ValidateRefreshRequestAsync(
        string accessToken,
        string refreshToken,
        CancellationToken cancellationToken)
    {
        var claimsPrincipal = GetPrincipalFromExpiredToken(accessToken);

        if (claimsPrincipal is null)
            return Result.Fail<ClaimsPrincipal>(TokensErrors
                .InvalidToken);
        
        var expiryDateUnix = long.Parse(claimsPrincipal.Claims
                .Single(x => x.Type == JwtRegisteredClaimNames.Exp)
                .Value);
        var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddSeconds(expiryDateUnix);
        
        if (expiryDateTimeUtc > DateTime.UtcNow)
            return Result.Fail<ClaimsPrincipal>(TokensErrors
                .TokenIsNotExpired);
        
        string jti = claimsPrincipal.Claims
            .Single(x => x.Type == CustomClaims.Jti)
            .Value;
        var storedRefreshToken = await _refreshTokensRepository
            .GetRefreshTokenAsync(refreshToken, cancellationToken);

        if (storedRefreshToken is null ||
            DateTime.UtcNow > storedRefreshToken.ExpiryDate ||
            storedRefreshToken.JwtId != jti)
        {
            return Result.Fail<ClaimsPrincipal>(TokensErrors
                .InvalidToken);
        }
        
        return Result.Ok(claimsPrincipal);
    }
    
    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var originalValidator = _tokenValidationParameters.LifetimeValidator;
            _tokenValidationParameters.LifetimeValidator = (
                notBefore, expires, securityToken, validationParameters) => true;
            
            var principal = tokenHandler.ValidateToken(
                token,
                _tokenValidationParameters,
                out var validatedToken);
            
            _tokenValidationParameters.LifetimeValidator = originalValidator;
            
            if (!JwtHasValidSecurityAlgorithm(validatedToken))
                return null;
            return principal;
        }
        catch
        {
            return null;
        }
    }
    
    private bool JwtHasValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
               jwtSecurityToken.Header.Alg.Equals(
                   SecurityAlgorithms.HmacSha256,
                   StringComparison.InvariantCultureIgnoreCase);
    }
}