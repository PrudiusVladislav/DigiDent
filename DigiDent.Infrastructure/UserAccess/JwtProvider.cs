using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DigiDent.Application.UserAccess;
using DigiDent.Domain.UserAccessContext.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DigiDent.Infrastructure.UserAccess;

public class JwtProvider: IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private static readonly DateTime ExpirationTime = DateTime.UtcNow.AddHours(1);
    
    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }
    
    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
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
            expires: ExpirationTime,
            signingCredentials: signingCredentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}