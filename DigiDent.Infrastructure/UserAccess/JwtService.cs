using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Application.UserAccess.Commands.Refresh;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Application.UserAccess.Tokens;
using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DigiDent.Infrastructure.UserAccess;

public class JwtService: IJwtService
{
    private readonly JwtOptions _jwtOptions;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly IRefreshTokensRepository _refreshTokensRepository;
    private readonly IUsersRepository _usersRepository;
    
    public JwtService(
        IOptions<JwtOptions> jwtOptions,
        TokenValidationParameters tokenValidationParameters,
        IRefreshTokensRepository refreshTokensRepository,
        IUsersRepository usersRepository)
    {
        _jwtOptions = jwtOptions.Value;
        _tokenValidationParameters = tokenValidationParameters;
        _refreshTokensRepository = refreshTokensRepository;
        _usersRepository = usersRepository;
    }
    
    public async Task<AuthenticationResponse> GenerateAuthenticationResponseAsync(
        User user,
        CancellationToken cancellationToken)
    {
        //TODO: make check if the token already exists
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email.Value),
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
        var accessToken = new JwtSecurityTokenHandler()
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
        //TODO: for some reason token is not saved to the database
        await _refreshTokensRepository
            .AddRefreshTokenAsync(refreshToken, cancellationToken);
        
        return new AuthenticationResponse(accessToken, refreshToken.Token);
    }

    public async Task<Result<AuthenticationResponse>> RefreshTokenAsync(
        RefreshCommand request,
        CancellationToken cancellationToken)
    {
        var claimsPrincipal = GetPrincipalFromExpiredToken(request.AccessToken);

        if (claimsPrincipal is null)
            return Result.Fail<AuthenticationResponse>(TokensErrors
                .InvalidToken);
        
        var expiryDateUnix =
            long.Parse(claimsPrincipal.Claims
                .Single(x => x.Type == JwtRegisteredClaimNames.Exp)
                .Value);
        var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddSeconds(expiryDateUnix);
        
        if (expiryDateTimeUtc > DateTime.UtcNow)
            return Result.Fail<AuthenticationResponse>(TokensErrors
                .TokenIsNotExpired);
        
        var jti = claimsPrincipal.Claims
            .Single(x => x.Type == JwtRegisteredClaimNames.Jti)
            .Value;
        var storedRefreshToken = await _refreshTokensRepository
            .GetRefreshTokenAsync(request.RefreshToken, cancellationToken);

        if (storedRefreshToken is null ||
            DateTime.UtcNow > storedRefreshToken.ExpiryDate ||
            storedRefreshToken.JwtId != jti)
        {
            return Result.Fail<AuthenticationResponse>(TokensErrors
                .InvalidToken);
        }
        
        await _refreshTokensRepository.DeleteRefreshTokenAsync(
            storedRefreshToken.Token,
            cancellationToken);

        Console.WriteLine(claimsPrincipal.Claims.Select(c => c.Type));
        User user = (await _usersRepository.GetByIdAsync(
            new UserId(
                Guid.Parse(claimsPrincipal
                    .Claims.Single(x => x.Type == JwtRegisteredClaimNames.Sub)
                    .Value)), cancellationToken))!;
        return Result.Ok(await GenerateAuthenticationResponseAsync(
            user, cancellationToken));
    }
    
    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            _tokenValidationParameters.ValidateLifetime = false;
            var principal = tokenHandler.ValidateToken(
                token,
                _tokenValidationParameters,
                out var validatedToken);
            _tokenValidationParameters.ValidateLifetime = true;
            
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